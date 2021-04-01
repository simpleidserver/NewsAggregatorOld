using Microsoft.Extensions.Options;
using Microsoft.ML;
using Microsoft.ML.Data;
using NewsAggregator.Core.Repositories;
using NewsAggregator.ML.Helpers;
using NewsAggregator.ML.Models;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.ML.Articles.Operations
{
    public class TrainLDAOperation : IArticleOperation
    {
        private readonly IArticleQueryRepository _articleQueryRepository;
        private readonly NewsAggregatorMLOptions _options;

        public TrainLDAOperation(
            IArticleQueryRepository articleQueryRepository,
            IOptions<NewsAggregatorMLOptions> options)
        {
            _articleQueryRepository = articleQueryRepository;
            _options = options.Value;
        }

        public Task Execute(string language, CancellationToken cancellationToken)
        {
            var mlContext = new MLContext();
            var sqlCommand = _articleQueryRepository.GetArticlesByLanguageSQL(language);
            var dbSource = new DatabaseSource(SqlClientFactory.Instance, _options.ConnectionString, sqlCommand);
            var loader = mlContext.Data.CreateDatabaseLoader<ArticleData>();
            var fullDataView = loader.Load(dbSource);
            var textPipeline = mlContext.Transforms.Text
                .NormalizeText("NormalizedText", "Text")
                 .Append(mlContext.Transforms.Text.TokenizeIntoWords("Tokens", "NormalizedText")) // Extract tokens.
                 .Append(mlContext.Transforms.Text.RemoveDefaultStopWords("Tokens")) // Remove stop words.
                 .Append(mlContext.Transforms.Conversion.MapValueToKey("Tokens"))  // Convert keyword to integer (vocubulary)
                 .Append(mlContext.Transforms.Text.ProduceNgrams("Tokens")) // Produces a vector of counts of n-gram
                 .Append(mlContext.Transforms.Text.LatentDirichletAllocation("Features", "Tokens", numberOfTopics: _options.LDANbTopics)); // Create a LDA (uses LightLDA) to transform text into a vector of Single indicating the similarity of the text with each topic identified.
            var textTransformer = textPipeline.Fit(fullDataView);
            mlContext.Model.Save(textTransformer, fullDataView.Schema, DirectoryHelper.GetLDAArticles(language));
            return Task.CompletedTask;
        }
    }
}
