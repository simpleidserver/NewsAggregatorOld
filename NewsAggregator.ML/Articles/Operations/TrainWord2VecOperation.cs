using Microsoft.Extensions.Logging;
using Microsoft.ML;
using NewsAggregator.Domain.Articles;
using NewsAggregator.Domain.Articles.Events;
using NewsAggregator.ML.Factories;
using NewsAggregator.ML.Helpers;
using NewsAggregator.ML.Models;
using NewsAggregator.ML.Resources;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.ML.Articles.Operations
{
    public class TrainWord2VecOperation : IOperation
    {
        private readonly ArticleAggregate _article;
        private readonly NewsAggregatorMLOptions _options;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ArticleManager> _logger;

        public TrainWord2VecOperation(
            ArticleAggregate article,
            NewsAggregatorMLOptions options,
            IHttpClientFactory httpClientFactory,
             ILogger<ArticleManager> logger)
        {
            _article = article;
            _options = options;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public virtual async Task Execute(CancellationToken cancellationToken)
        {
            var vectorFilePath = await DownloadAndExtractWordEmbedding(_article.Language, cancellationToken);
            if(Directory.Exists(DirectoryHelper.GetTrainedWordEmbeddingFilePath(_article.Language)))
            {
                return;
            }

            var mlContext = new MLContext();
            var fullDataView = mlContext.Data.LoadFromTextFile<ArticleData>(DirectoryHelper.GetCSVArticles(_article.Language), hasHeader: false, separatorChar: ',');
            var textPipeline = mlContext.Transforms.Text.NormalizeText("Text")
                .Append(mlContext.Transforms.Text.TokenizeIntoWords("Tokens", "Text"))
                .Append(mlContext.Transforms.Text.ApplyWordEmbedding("Features", vectorFilePath, "Tokens"));
            var trainedModel = textPipeline.Fit(fullDataView);
            mlContext.Model.Save(trainedModel, fullDataView.Schema, DirectoryHelper.GetTrainedWordEmbeddingFilePath(_article.Language));
        }

        public void Rollback() { }

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
                    var progress = new Progress<float>();
                    progress.ProgressChanged += HandleProgressChanged;
                    using (var file = File.OpenWrite(DirectoryHelper.GetWordEmbeddingZipFilePath(language)))
                    {
                        await httpClient.DownloadDataAsync(wordEmbeddingOptions.DownloadUrl, file, progress);
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

        private void HandleProgressChanged(object sender, float e)
        {
            _logger.LogInformation(string.Format(Global.DownloadingDocument, e));
        }
    }
}
