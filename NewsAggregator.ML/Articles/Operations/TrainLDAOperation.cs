using Microsoft.ML;
using NewsAggregator.Domain.Articles.Events;
using NewsAggregator.ML.Helpers;
using NewsAggregator.ML.Models;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.ML.Articles.Operations
{
    public class TrainLDAOperation : IOperation
    {
        private readonly ArticleAddedEvent _evt;
        private readonly NewsAggregatorMLOptions _options;

        public TrainLDAOperation(
            ArticleAddedEvent evt,
            NewsAggregatorMLOptions options)
        {
            _evt = evt;
            _options = options;
        }

        public Task Execute(CancellationToken cancellationToken)
        {
            var mlContext = new MLContext();
            var fullDataView = mlContext.Data.LoadFromTextFile<ArticleData>(DirectoryHelper.GetCSVArticles(_evt.Language), hasHeader: false, separatorChar: ',');
            var textPipeline = mlContext.Transforms.Text
                .NormalizeText("NormalizedText", "Text")
                 .Append(mlContext.Transforms.Text.TokenizeIntoWords("Tokens", "NormalizedText")) // Extract tokens.
                 .Append(mlContext.Transforms.Text.RemoveDefaultStopWords("Tokens")) // Remove stop words.
                 .Append(mlContext.Transforms.Conversion.MapValueToKey("Tokens"))  // Convert keyword to integer (vocubulary)
                 .Append(mlContext.Transforms.Text.ProduceNgrams("Tokens")) // Produces a vector of counts of n-gram
                 .Append(mlContext.Transforms.Text.LatentDirichletAllocation("Features", "Tokens", numberOfTopics: _options.LDANbTopics)); // Create a LDA (uses LightLDA) to transform text into a vector of Single indicating the similarity of the text with each topic identified.
            var textTransformer = textPipeline.Fit(fullDataView);
            mlContext.Model.Save(textTransformer, fullDataView.Schema, DirectoryHelper.GetLDAArticles(_evt.Language));
            return Task.CompletedTask;
        }

        public void Rollback() { }
    }
}
