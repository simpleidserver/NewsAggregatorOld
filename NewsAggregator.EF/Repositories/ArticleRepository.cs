using NewsAggregator.Core.Repositories;
using NewsAggregator.Domain.Articles;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.EF.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly NewsAggregatorDBContext _dbContext;

        public ArticleRepository(NewsAggregatorDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<ArticleAggregate> Query()
        {
            return _dbContext.Articles;
        }

        public Task Add(ArticleAggregate article, CancellationToken cancellationToken)
        {
            _dbContext.Add(article);
            return Task.CompletedTask;
        }

        public Task<int> SaveChanges(CancellationToken cancellationToken)
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
