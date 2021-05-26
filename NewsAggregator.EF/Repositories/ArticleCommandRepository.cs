using Microsoft.EntityFrameworkCore;
using NewsAggregator.Core.Domains.Articles;
using NewsAggregator.Core.Repositories;
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

        public Task<ArticleAggregate> Get(string articleId, CancellationToken cancellationToken)
        {
            return _dbContext.Articles
                .Include(a => a.ArticleLikeLst)
                .Include(a => a.ArticleReadLst)
                .FirstOrDefaultAsync(a => a.Id == articleId, cancellationToken);
        }

        public Task Add(ArticleAggregate article, CancellationToken cancellationToken)
        {
            _dbContext.Articles.Add(article);
            return Task.CompletedTask;
        }

        public Task Update(ArticleAggregate article, CancellationToken cancellationToken)
        {
            _dbContext.Articles.Update(article);
            return Task.CompletedTask;
        }

        public Task<int> SaveChanges(CancellationToken cancellationToken)
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
