using NewsAggregator.Core.Repositories;
using NewsAggregator.Domain.Articles;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.EF.Repositories
{
    public class ArticleCommandRepository : IArticleCommandRepository
    {
        private readonly NewsAggregatorDBContext _dbContext;

        public ArticleCommandRepository(NewsAggregatorDBContext dbContext)
        {
            _dbContext = dbContext;
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
