using Accord.Math.Distances;
using Microsoft.Extensions.Options;
using Microsoft.ML;
using NewsAggregator.Domain.Sessions.Enums;
using NewsAggregator.ML.Helpers;
using NewsAggregator.ML.Infrastructures.Jobs;
using NewsAggregator.ML.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.ML.Jobs
{
    public class NextArticleRecommenderJob : INextArticleRecommenderJob
    {
        private readonly NewsAggregatorMLOptions _options;

        public NextArticleRecommenderJob(IOptions<NewsAggregatorMLOptions> options)
        {
            _options = options.Value;
        }

        public Task Run(CancellationToken cancellationToken)
        {
            var popularArticles = GetPopularArticles();
            var result = GetSimilarArticlesByWord2Vec(popularArticles).ToList();
            // Store popular articles per session and user.
            return Task.CompletedTask;
        }

        protected virtual IEnumerable<PopularArticle> GetPopularArticles()
        {
            var lst = new List<PopularArticle>();
            var sessionFiles = DirectoryHelper.GetSessionFiles();
            var lstGroup = sessionFiles.GroupBy(s => s.PersonId);
            foreach (var grp in lstGroup)
            {
                var latestSessions = grp.OrderByDescending(f => f.CreateDateTime).Take(_options.NbSessionsToProcess);
                foreach (var session in latestSessions)
                {
                    lst.AddRange(GetPopularArticles(session.PersonId, session.FilePath)
                        .OrderByDescending(a => a.Score)
                        .Take(_options.NbArticlesToProcess));
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
            foreach (var grp in features.GroupBy(f => f.ArticleId))
            {
                var sumPonderation = grp.Sum(p => p.Ponderation);
                var score = Math.Log(1 + sumPonderation, 2);
                result.Add(new PopularArticle(personId, grp.Key, grp.First().ArticleLanguage, score));
            }

            return result;
        }

        protected virtual IEnumerable<IEnumerable<RecommendedArticle>> GetSimilarArticlesByWord2Vec(IEnumerable<PopularArticle> articles)
        {
            foreach (var grp in articles.OrderByDescending(a => a.Score).GroupBy(a => a.ArticleLanguage))
            {
                var firstArticle = grp.First();
                var articleCSVPath = DirectoryHelper.GetCSVArticles(firstArticle.ArticleLanguage);
                var wordEmbeddingPath = DirectoryHelper.GetTrainedWordEmbeddingFilePath(firstArticle.ArticleLanguage);
                var mlContext = new MLContext();
                ITransformer trainedModel = mlContext.Model.Load(wordEmbeddingPath, out DataViewSchema sc);
                var predictionEngine = mlContext.Model.CreatePredictionEngine<ArticleData, TransformedArticleData>(trainedModel);
                var transformedArticles = GetTransformedArticles(predictionEngine, firstArticle.PersonId, firstArticle.ArticleLanguage);
                yield return CalculateSimilarityWithCOSIN(transformedArticles, grp.ToList())
                    .Distinct()
                    .OrderByDescending(o => o.Score);
            }
        }

        protected virtual IEnumerable<RecommendedArticle> CalculateSimilarityWithCOSIN(IEnumerable<TransformedArticleData> transformedArticles, IEnumerable<PopularArticle> popularArticles)
        {
            var cosine = new Cosine();
            foreach (var popularArticle in popularArticles)
            {
                var selectedArticleVector = transformedArticles.First(a => a.ExternalId == popularArticle.ArticleId).Features;
                foreach (var transformedArticle in transformedArticles)
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
            var csvFileReader = new CSVFileReader();
            foreach (var articles in csvFileReader.Read<ArticleData>(DirectoryHelper.GetCSVArticles(language)))
            {
                foreach(var article in articles)
                {
                    var transformedText = predictionEngine.Predict(article);
                    yield return transformedText;
                }
            }
        }

        protected static double[] ConvertToDouble(float[] f)
        {
            return f.Select(_ => (double)new decimal(_)).ToArray();
        }
    }
}
