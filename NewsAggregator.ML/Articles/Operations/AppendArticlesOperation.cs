using NewsAggregator.Domain.Articles.Events;
using NewsAggregator.ML.Exceptions;
using NewsAggregator.ML.Helpers;
using NewsAggregator.ML.Models;
using NewsAggregator.ML.Resources;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.ML.Articles.Operations
{
    public class AppendArticlesOperation : IOperation
    {
        private readonly IEnumerable<ArticleAddedEvent> _evts;

        public AppendArticlesOperation(IEnumerable<ArticleAddedEvent> evts)
        {
            _evts = evts;
        }

        public Task Execute(CancellationToken cancellationToken)
        {
            var firstArticle = _evts.First();
            var csvFilePath = DirectoryHelper.GetCSVArticles(firstArticle.Language);
            var csvFileReader = new CSVFileReader();
            var externalIds = _evts.Select(e => e.ExternalId);
            if (csvFileReader.TryGetRecord(csvFilePath, art => externalIds.Contains(art.ExternalId), out ArticleData articleData))
            {
                throw new InternalArticleOperationException(Global.ArticlesAlreadyExists);
            }

            DirectoryHelper.CreateFile(csvFilePath);
            using (var streamWriter = new StreamWriter(csvFilePath))
            {
                foreach(var evt in _evts)
                {
                    streamWriter.WriteLine($"{evt.Id},{evt.ExternalId},\"{evt.Title} {evt.Summary}\"");
                }
            }

            return Task.CompletedTask;
        }

        public void Rollback()
        {
            var firstArticle = _evts.First();
            var csvFilePath = DirectoryHelper.GetCSVArticles(firstArticle.Language);
            var tmpCsvFilePath = DirectoryHelper.GetTmpCSVArticles(firstArticle.Language);
            DirectoryHelper.CreateFile(tmpCsvFilePath);
            var csvFileReader = new CSVFileReader();
            var externalIds = _evts.Select(e => e.ExternalId);
            using (var streamWriter = new StreamWriter(tmpCsvFilePath))
            {
                foreach (var records in csvFileReader.Read<ArticleData>(csvFilePath))
                {
                    foreach(var record in records)
                    {
                        if (!externalIds.Contains(record.Id))
                        {
                            streamWriter.WriteLine($"{record.Id},{record.ExternalId},{record.Text}");
                        }
                    }
                }
            }

            File.Delete(csvFilePath);
            File.Move(tmpCsvFilePath, csvFilePath);
            File.Delete(tmpCsvFilePath);
        }
    }
}
