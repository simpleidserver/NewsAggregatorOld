using NewsAggregator.Domain.Articles;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Core.Repositories
{
    public interface IArticleRepository
    {
        IQueryable<ArticleAggregate> Query();
        Task Add(ArticleAggregate article, CancellationToken cancellationToken);
        Task<int> SaveChanges(CancellationToken cancellationToken);
    }
}
