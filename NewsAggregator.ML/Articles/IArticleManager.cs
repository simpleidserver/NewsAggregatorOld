using NewsAggregator.Domain.Articles.Events;
using System.Collections.Generic;

namespace NewsAggregator.ML.Articles
{
    public interface IArticleManager
    {
        void AddArticles(IEnumerable<ArticleAddedEvent> evts);
    }
}
