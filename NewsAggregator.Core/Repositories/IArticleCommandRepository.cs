using NewsAggregator.Core.Domains.Articles;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Core.Repositories
{
    public interface IArticleCommandRepository
    {
        Task<ArticleAggregate> Get(string articleId, CancellationToken cancellationToken);
        Task Add(ArticleAggregate article, CancellationToken cancellationToken);
        Task Update(ArticleAggregate article, CancellationToken cancellationToken);
        Task<int> SaveChanges(CancellationToken cancellationToken);
    }
}
