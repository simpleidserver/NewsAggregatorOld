using Accord.Math.Distances;
using Medallion.Threading.FileSystem;
using Microsoft.Extensions.Options;
using Microsoft.ML;
using Microsoft.ML.Data;
using NewsAggregator.Core.Domains.Recommendations;
using NewsAggregator.Core.Domains.Sessions.Enums;
using NewsAggregator.Core.Repositories;
using NewsAggregator.ML.Helpers;
using NewsAggregator.ML.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.ML.Jobs
{
    public class NextArticleRecommenderJob : INextArticleRecommenderJob
    {
        private readonly NewsAggregatorMLOptions _options;
        private readonly IArticleQueryRepository _articleQueryRepository;
        private readonly ISessionQueryRepository _sessionQueryRepository;
        private readonly IRecommendationCommandRepository _recommendationCommandRepository;

        public NextArticleRecommenderJob(
            IOptions<NewsAggregatorMLOptions> options,
            IArticleQueryRepository articleQueryRepository,
            ISessionQueryRepository sessionQueryRepository,
            IRecommendationCommandRepository recommendationCommandRepository)
        {
            _options = options.Value;
            _articleQueryRepository = articleQueryRepository;
            _sessionQueryRepository = sessionQueryRepository;
            _recommendationCommandRepository = recommendationCommandRepository;
        }

        public async Task Run(CancellationToken cancellationToken)
        {
            var directoryInfo = ArticleExtractorJob.GetDirectory();
            var lck = new FileDistributedLock(directoryInfo, "extract-recommendations");
            using (var distributedLock = await lck.TryAcquireAsync())
            {
                if (distributedLock == null)
                {
                    return;
                }

                var popularArticles = await GetPopularArticles(cancellationToken);
                var result = GetSimilarArticlesByWord2Vec(popularArticles).ToList();
                foreach (var articles in result)
                {
                    var firstArticle = articles.First();
                    var recommendation = RecommendationAggregate.Create(firstArticle.UserId);
                    foreach (var article in articles)
                    {
                        recommendation.Recommend(article.ArticleId, article.Score);
                    }

                    await _recommendationCommandRepository.Add(recommendation, cancellationToken);
                    await _recommendationCommandRepository.SaveChanges(cancellationToken);
                }
            }
        }

        protected virtual async Task<IEnumerable<PopularArticle>> GetPopularArticles(CancellationToken cancellationToken)
        {
            var lst = new List<PopularArticle>();
            var sessionFiles = await _sessionQueryRepository.GetAll(cancellationToken);
            var lstGroup = sessionFiles.GroupBy(s => s.UserId);
            foreach (var grp in lstGroup)
            {
                var latestSessions = grp.OrderByDescending(f => f.CreateDateTime).Take(_options.NbSessionsToProcess);
                foreach (var session in latestSessions)
                {
                    lst.AddRange(GetPopularArticles(session.UserId, session.Id)
                        .OrderByDescending(a => a.Score)
                        .Take(_options.NbArticlesToProcess));
                }
            }

            return lst;
        }

        protected virtual ICollection<PopularArticle> GetPopularArticles(string personId, string sessionId)
        {
            var result = new List<PopularArticle>();
            var mlContext = new MLContext();
            var sqlCommand = _sessionQueryRepository.GetSessionActionsSQL(sessionId);
            var dbSource = new DatabaseSource(SqlClientFactory.Instance, _options.ConnectionString, sqlCommand);
            var loader = mlContext.Data.CreateDatabaseLoader<SessionData>();
            var fullDataView = loader.Load(dbSource);
            var lookupMap = mlContext.Data.LoadFromEnumerable(InteractionTypes.InteractionTypeLookup);
            var pipeline = mlContext.Transforms.Conversion.MapValue("Ponderation", lookupMap, lookupMap.Schema["Key"], lookupMap.Schema["Ponderation"], "InteractionType");
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
            foreach (var grpUser in articles.OrderByDescending(a => a.UserId).GroupBy(a => a.UserId))
            {
                var result = new List<RecommendedArticle>();
                foreach(var grp in grpUser.OrderByDescending(a => a.Score).GroupBy(a => a.ArticleLanguage))
                {
                    var firstArticle = grp.First();
                    var wordEmbeddingPath = DirectoryHelper.GetTrainedWordEmbeddingFilePath(firstArticle.ArticleLanguage);
                    var mlContext = new MLContext();
                    ITransformer trainedModel = mlContext.Model.Load(wordEmbeddingPath, out DataViewSchema sc);
                    var predictionEngine = mlContext.Model.CreatePredictionEngine<ArticleData, TransformedArticleData>(trainedModel);
                    // TODO : GET ALL ARTICLES EXCEPT THE ONE VISISTED BY THE USER ...
                    var transformedArticles = GetTransformedArticles(predictionEngine, firstArticle.UserId, firstArticle.ArticleLanguage);
                    result.AddRange(CalculateSimilarityWithCOSIN(transformedArticles, grp.ToList())
                        .Distinct()
                        .OrderByDescending(o => o.Score));
                }

                yield return result;
            }
        }

        protected virtual IEnumerable<RecommendedArticle> CalculateSimilarityWithCOSIN(IEnumerable<TransformedArticleData> transformedArticles, IEnumerable<PopularArticle> popularArticles)
        {
            var cosine = new Cosine();
            foreach (var popularArticle in popularArticles)
            {
                var selectedArticleVector = transformedArticles.First(a => a.Id == popularArticle.ArticleId).Features;
                foreach (var transformedArticle in transformedArticles)
                {
                    if (popularArticle.ArticleId == transformedArticle.Id)
                    {
                        continue;
                    }

                    var similarity = cosine.Similarity(ConvertToDouble(selectedArticleVector), ConvertToDouble(transformedArticle.Features));
                    yield return new RecommendedArticle { ArticleId = transformedArticle.Id, UserId = popularArticle.UserId, Score = popularArticle.Score + similarity };
                }
            }
        }

        protected virtual IEnumerable<TransformedArticleData> GetTransformedArticles(PredictionEngine<ArticleData, TransformedArticleData> predictionEngine, string personId, string language)
        {
            foreach(var articles in _articleQueryRepository.GetAll(language))
            {
                foreach(var article in articles)
                {
                    var transformedText = predictionEngine.Predict(ArticleData.Transform(article));
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
