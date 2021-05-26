using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsAggregator.Core.Domains.DataSources
{
    public class DataSourceAggregate : BaseAggregate
    {
        private DataSourceAggregate()
        {
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public int NbFollowers { get; set; }
        public int NbStoriesPerMonth { get; set; }
        public ICollection<DataSourceExtractionHistory> ExtractionHistories { get; set; }
        public ICollection<DataSourceArticle> Articles { get; set; }
        public ICollection<DataSourceTopic> Topics { get; set; }

        public bool IsArticleExtracted(DateTimeOffset publicationDate)
        {
            var extractionHistory = ExtractionHistories.OrderByDescending(h => h.LastArticlePublicationDate).FirstOrDefault();
            if (extractionHistory == null)
            {
                return false;
            }

            return extractionHistory.LastArticlePublicationDate >= publicationDate;
        }

        public void AddHistory(DateTimeOffset lastArticlePublicationDate, int nbExtractedArticles)
        {
            ExtractionHistories.Add(DataSourceExtractionHistory.Create(lastArticlePublicationDate, nbExtractedArticles));
        }

        public void IncrementFollower()
        {
            NbFollowers++;
        }

        public void DecrementFollower()
        {
            NbFollowers--;
        }

        public void IncrementTopic(string topicName)
        {
            var topic = Topics.FirstOrDefault(t => t.TopicName == topicName);
            if (topic == null)
            {
                Topics.Add(DataSourceTopic.Create(topicName));
            } 
            else
            {
                topic.Increment();
            }
        }

        public void AddArticle(DateTimeOffset publishDateTime)
        {
            var month = publishDateTime.Month;
            var year = publishDateTime.Year;
            var article = Articles.FirstOrDefault(a => a.Month == month && a.Year == year);
            if (article == null)
            {
                article = DataSourceArticle.Create(month, year);
                Articles.Add(article);
            }

            article.Increment();
            NbStoriesPerMonth = Articles.Sum(a => a.NbArticles) / Articles.Count();
        }

        public static DataSourceAggregate Create(string title, string description, string url)
        {
            var result = new DataSourceAggregate
            {
                Id = Guid.NewGuid().ToString(),
                Title = title,
                Description = description,
                Url = url
            };
            return result;
        }

        protected override void Handle(dynamic evt)
        {
            throw new System.NotImplementedException();
        }
    }
}