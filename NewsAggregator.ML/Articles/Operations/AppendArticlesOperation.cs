using NewsAggregator.Domain.Articles;
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
        private readonly IEnumerable<ArticleAggregate> _articles;

        public AppendArticlesOperation(IEnumerable<ArticleAggregate> articles)
        {
            _articles = articles;
        }

        public Task Execute(CancellationToken cancellationToken)
        {
            var firstArticle = _articles.First();
            var csvFilePath = DirectoryHelper.GetCSVArticles(firstArticle.Language);
            var csvFileReader = new CSVFileReader();
            var externalIds = _articles.Select(e => e.ExternalId);
            if (csvFileReader.TryGetRecord(csvFilePath, art => externalIds.Contains(art.ExternalId), out ArticleData articleData))
            {
                throw new InternalArticleOperationException(Global.ArticlesAlreadyExists);
            }

            DirectoryHelper.CreateFile(csvFilePath);
            using (var streamWriter = new StreamWriter(csvFilePath))
            {
                foreach(var article in _articles)
                {
                    streamWriter.WriteLine($"{article.Id},{article.ExternalId},\"{article.Title} {article.Summary}\"");
                }
            }

            return Task.CompletedTask;
        }

        public void Rollback()
        {
            var firstArticle = _articles.First();
            var csvFilePath = DirectoryHelper.GetCSVArticles(firstArticle.Language);
            var tmpCsvFilePath = DirectoryHelper.GetTmpCSVArticles(firstArticle.Language);
            DirectoryHelper.CreateFile(tmpCsvFilePath);
            var csvFileReader = new CSVFileReader();
            var externalIds = _articles.Select(e => e.ExternalId);
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
