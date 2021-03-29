using NewsAggregator.Domain.Articles;
using System.Collections.Generic;

namespace NewsAggregator.ML.Articles
{
    public interface IArticleManager
    {
        void AddArticles(IEnumerable<ArticleAggregate> articles);
    }
}
