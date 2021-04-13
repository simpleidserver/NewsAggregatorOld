using NewsAggregator.Core.QueryResults;
using NewsAggregator.Core.Repositories.Parameters;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Core.Repositories
{
    public interface IArticleQueryRepository
    {
        string GetArticlesByLanguageSQL(string language);
        IEnumerable<IEnumerable<ArticleQueryResult>> GetAll(string language, int count = 500);
        Task<SearchQueryResult<ArticleQueryResult>> SearchInDataSource(SearchArticlesInDataSourceParameter parameter, CancellationToken cancellationToken);
        Task<SearchQueryResult<ArticleQueryResult>> SearchInFeed(SearchArticlesInFeedParameter parameter, CancellationToken cancellationToken);
    }
}
