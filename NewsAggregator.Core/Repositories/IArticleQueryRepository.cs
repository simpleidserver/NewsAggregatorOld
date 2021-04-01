using NewsAggregator.Core.Parameters;
using NewsAggregator.Core.QueryResults;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Core.Repositories
{
    public interface IArticleQueryRepository
    {
        string GetArticlesByLanguageSQL(string language);
        IEnumerable<IEnumerable<ArticleQueryResult>> GetAll(string language, int count = 500);
        Task<SearchQueryResult<ArticleQueryResult>> SearchInFeeds(SearchArticlesInFeedParameter parameter, CancellationToken cancellationToken);
    }
}
