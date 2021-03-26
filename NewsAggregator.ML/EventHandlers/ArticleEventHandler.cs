using Microsoft.Extensions.Options;
using Microsoft.ML;
using NewsAggregator.Domain;
using NewsAggregator.Domain.Articles.Events;
using NewsAggregator.ML.Factories;
using NewsAggregator.ML.Helpers;
using NewsAggregator.ML.Models;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.ML.EventHandlers
{
    public class ArticleEventHandler : IEventHandler<ArticleAddedEvent>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly NewsAggregatorMLOptions _options;

        public ArticleEventHandler(IHttpClientFactory httpClientFactory,
            IOptions<NewsAggregatorMLOptions> options)
        {
            _httpClientFactory = httpClientFactory;
            _options = options.Value;
        }

        #region Handle events

        public async Task Handle(ArticleAddedEvent evt, CancellationToken cancellationToken)
        {
            var path = EnrichCSVFile(evt);
            await TrainWord2Vec(path, evt, cancellationToken);
            await TrainLDA(path, evt, cancellationToken);
        }

        #endregion

        protected virtual async Task TrainWord2Vec(string path, ArticleAddedEvent evt, CancellationToken cancellation)
        {
            var vectorFilePath = await DownloadAndExtractWordEmbedding(evt.Language, cancellation);
            var mlContext = new MLContext();
            var fullDataView = mlContext.Data.LoadFromTextFile<ArticleData>(path, hasHeader: false, separatorChar: ',');
            var textPipeline = mlContext.Transforms.Text.NormalizeText("Text")
                .Append(mlContext.Transforms.Text.TokenizeIntoWords("Tokens", "Text"))
                .Append(mlContext.Transforms.Text.ApplyWordEmbedding("Features", vectorFilePath, "Tokens"));
            var trainedModel = textPipeline.Fit(fullDataView);
            mlContext.Model.Save(trainedModel, fullDataView.Schema, DirectoryHelper.GetTrainedWordEmbeddingFilePath(evt.Language));
        }

        protected virtual Task TrainLDA(string path, ArticleAddedEvent evt, CancellationToken cancellationToken)
        {
            var mlContext = new MLContext();
            var fullDataView = mlContext.Data.LoadFromTextFile<ArticleData>(path, hasHeader: false, separatorChar: ',');
            var textPipeline = mlContext.Transforms.Text
                .NormalizeText("NormalizedText", "Text")
                 .Append(mlContext.Transforms.Text.TokenizeIntoWords("Tokens", "NormalizedText")) // Extract tokens.
                 .Append(mlContext.Transforms.Text.RemoveDefaultStopWords("Tokens")) // Remove stop words.
                 .Append(mlContext.Transforms.Conversion.MapValueToKey("Tokens"))  // Convert keyword to integer (vocubulary)
                 .Append(mlContext.Transforms.Text.ProduceNgrams("Tokens")) // Produces a vector of counts of n-gram
                 .Append(mlContext.Transforms.Text.LatentDirichletAllocation("Features", "Tokens", numberOfTopics: _options.LDANbTopics)); // Create a LDA (uses LightLDA) to transform text into a vector of Single indicating the similarity of the text with each topic identified.
            var textTransformer = textPipeline.Fit(fullDataView);
            mlContext.Model.Save(textTransformer, fullDataView.Schema, DirectoryHelper.GetLDAArticles(evt.Language));
            return Task.CompletedTask;
        }

        protected virtual string EnrichCSVFile(ArticleAddedEvent evt)
        {
            var csvFilePath = DirectoryHelper.GetCSVArticles(evt.Language);
            DirectoryHelper.CreateFile(csvFilePath);
            File.WriteAllLines(csvFilePath, new string[] { $"{evt.Id},{evt.Title} {evt.Summary}" });
            return csvFilePath;
        }

        protected virtual async Task<string> DownloadAndExtractWordEmbedding(string language, CancellationToken cancellationToken)
        {
            var wordEmbeddingOptions = _options.WordEmbeddingOptions.First(_ => _.Language == language);
            var filePath = DirectoryHelper.GetWordEmbeddingFilePath(language);
            if (File.Exists(filePath))
            {
                return filePath;
            }

            var zipPath = DirectoryHelper.GetWordEmbeddingZipFilePath(language);
            if (!File.Exists(zipPath))
            {
                using (var httpClient = _httpClientFactory.BuildHttpClient())
                {
                    using (var response = await httpClient.GetAsync(wordEmbeddingOptions.DownloadUrl, cancellationToken))
                    {
                        using (var stream = await response.Content.ReadAsStreamAsync())
                        {
                            using (var w = File.OpenWrite(DirectoryHelper.GetWordEmbeddingZipFilePath(language)))
                            {
                                await stream.CopyToAsync(w);
                            }
                        }
                    }
                }
            }

            using (var archive = ZipFile.OpenRead(zipPath))
            {
                var entry = archive.Entries.First(e => e.FullName.EndsWith(".vec"));
                entry.ExtractToFile(filePath);
            }

            return filePath;
        }
    }
}
