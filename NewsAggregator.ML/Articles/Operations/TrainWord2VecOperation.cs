using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.ML;
using Microsoft.ML.Data;
using NewsAggregator.Core.Repositories;
using NewsAggregator.ML.Factories;
using NewsAggregator.ML.Helpers;
using NewsAggregator.ML.Models;
using NewsAggregator.ML.Resources;
using System;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.ML.Articles.Operations
{
    public class TrainWord2VecOperation : IArticleOperation
    {
        private readonly IArticleQueryRepository _articleQueryRepository;
        private readonly NewsAggregatorMLOptions _options;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<TrainWord2VecOperation> _logger;

        public TrainWord2VecOperation(
            IArticleQueryRepository articleQueryRepository,
            IHttpClientFactory httpClientFactory,
            ILogger<TrainWord2VecOperation> logger,
            IOptions<NewsAggregatorMLOptions> options)
        {
            _articleQueryRepository = articleQueryRepository;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _options = options.Value;
        }

        public virtual async Task Execute(string language, CancellationToken cancellationToken)
        {
            var vectorFilePath = await DownloadAndExtractWordEmbedding(language, cancellationToken);
            if(Directory.Exists(DirectoryHelper.GetTrainedWordEmbeddingFilePath(language)))
            {
                return;
            }

            var mlContext = new MLContext();
            string sqlCommand = _articleQueryRepository.GetArticlesByLanguageSQL(language);
            var dbSource = new DatabaseSource(SqlClientFactory.Instance, _options.ConnectionString, sqlCommand);
            var loader = mlContext.Data.CreateDatabaseLoader<ArticleData>();
            var fullDataView = loader.Load(dbSource);
            var textPipeline = mlContext.Transforms.Text.NormalizeText("Text")
                .Append(mlContext.Transforms.Text.TokenizeIntoWords("Tokens", "Text"))
                .Append(mlContext.Transforms.Text.ApplyWordEmbedding("Features", vectorFilePath, "Tokens"));
            var trainedModel = textPipeline.Fit(fullDataView);
            mlContext.Model.Save(trainedModel, fullDataView.Schema, DirectoryHelper.GetTrainedWordEmbeddingFilePath(language));
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
