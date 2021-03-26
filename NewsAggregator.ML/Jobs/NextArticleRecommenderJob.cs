using Accord.Math.Distances;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.ML;
using NewsAggregator.Domain.Sessions.Enums;
using NewsAggregator.ML.Helpers;
using NewsAggregator.ML.Infrastructures.Jobs;
using NewsAggregator.ML.Infrastructures.Locks;
using NewsAggregator.ML.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.ML.Jobs
{
    public class NextArticleRecommenderJob : BaseScheduledJob
    {
        public NextArticleRecommenderJob(
            IDistributedLock distributedLock, 
            IOptions<NewsAggregatorMLOptions> options, 
            ILogger<BaseScheduledJob> logger) : base(distributedLock, options, logger) { }

        protected override string LockName => "next-article-recommender";
        protected override int IntervalMS => 3600000;

        protected override Task Execute(CancellationToken token)
        {
            var popularArticles = GetPopularArticles();
            GetSimilarArticlesByWord2Vec(popularArticles);
            return Task.CompletedTask;
        }

        protected virtual IEnumerable<PopularArticle> GetPopularArticles()
        {
            var lst = new List<PopularArticle>();
            var sessionFiles = DirectoryHelper.GetSessionFiles();
            var lstGroup = sessionFiles.GroupBy(s => s.PersonId);
            foreach (var grp in lstGroup)
            {
                var latestSessions = grp.OrderByDescending(f => f.CreateDateTime).Take(Options.NbSessionsToProcess);
                foreach (var session in latestSessions)
                {
                    lst.AddRange(GetPopularArticles(session.PersonId, session.FilePath).OrderByDescending(a => a.Score).Take(Options.NbArticlesToProcess));
                }
            }

            return lst;
        }

        protected virtual ICollection<PopularArticle> GetPopularArticles(string personId, string path)
        {
            var result = new List<PopularArticle>();
            var mlContext = new MLContext();
            var fullDataView = mlContext.Data.LoadFromTextFile<SessionData>(path, hasHeader: false, separatorChar: ',');
            var lookupMap = mlContext.Data.LoadFromEnumerable(InteractionTypes.InteractionTypeLookup);
            var pipeline = mlContext.Transforms.Conversion.MapValue("Ponderation", lookupMap, lookupMap.Schema["Key"], lookupMap.Schema["Ponderation"], "EventType");
            var transformedData = pipeline.Fit(fullDataView).Transform(fullDataView);
            var features = mlContext.Data.CreateEnumerable<TransformedSessionData>(transformedData, reuseRowObject: false).ToList();
            foreach(var grp in features.GroupBy(f => f.ArticleId))
            {
                var sumPonderation = grp.Sum(p => p.Ponderation);
                var score = Math.Log(1 + sumPonderation, 2);
                result.Add(new PopularArticle(personId, grp.Key, grp.First().ArticleLanguage, score));
            }

            return result;
        }

        protected virtual IEnumerable<IEnumerable<RecommendedArticle>> GetSimilarArticlesByWord2Vec(IEnumerable<PopularArticle> articles)
        {
            foreach(var grp in articles.OrderByDescending(a => a.Score).GroupBy(a => a.ArticleLanguage))
            {
                var firstArticle = grp.First();
                var articleCSVPath = DirectoryHelper.GetCSVArticles(firstArticle.ArticleLanguage);
                // TODO : récupérer les articles qui n'ont pas encore été lues par l'utilisateur.
                var wordEmbeddingPath = DirectoryHelper.GetTrainedWordEmbeddingFilePath(firstArticle.ArticleLanguage);
                var mlContext = new MLContext();
                ITransformer trainedModel = mlContext.Model.Load(wordEmbeddingPath, out DataViewSchema sc);
                var predictionEngine = mlContext.Model.CreatePredictionEngine<ArticleData, TransformedArticleData>(trainedModel);
                var transformedArticles = GetTransformedArticles(predictionEngine, firstArticle.PersonId, firstArticle.ArticleLanguage);
                yield return CalculateSimilarityWithCOSIN(transformedArticles, grp.ToList());
            }
        }

        protected virtual IEnumerable<RecommendedArticle> CalculateSimilarityWithCOSIN(IEnumerable<TransformedArticleData> transformedArticles, IEnumerable<PopularArticle> popularArticles)
        {
            var cosine = new Cosine();
            foreach(var popularArticle in popularArticles)
            {
                var selectedArticleVector = transformedArticles.First(a => a.Id == popularArticle.ArticleId).Features;
                foreach(var transformedArticle in transformedArticles)
                {
                    if (popularArticle.ArticleId == transformedArticle.Id)
                    {
                        continue;
                    }

                    var similarity = cosine.Similarity(ConvertToDouble(selectedArticleVector), ConvertToDouble(transformedArticle.Features));
                    yield return new RecommendedArticle { ArticleId = transformedArticle.Id, PersonId = popularArticle.PersonId, Score = popularArticle.Score + similarity };
                }
            }
        }

        protected virtual IEnumerable<TransformedArticleData> GetTransformedArticles(PredictionEngine<ArticleData, TransformedArticleData> predictionEngine, string personId, string language)
        {
            foreach(var article in GetArticles(personId, language))
            {
                var transformedText = predictionEngine.Predict(article);
                yield return transformedText;
            }
        }

        protected virtual IEnumerable<ArticleData> GetArticles(string personId, string language)
        {
            var articleCSVPath = DirectoryHelper.GetCSVArticles(language);
            using (var file = new StreamReader(articleCSVPath))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    var splitted = line.Split(',');
                    yield return new ArticleData { Id = splitted[0], Text = splitted[1] };
                }
            }
        }

        protected static double[] ConvertToDouble(float[] f)
        {
            return f.Select(_ => (double)new decimal(_)).ToArray();
        }
    }
}
