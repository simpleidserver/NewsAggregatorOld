using NewsAggregator.Domain.Articles;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Core.Repositories.InMemory
{
    public class InMemoryArticleRepository : IArticleRepository
    {
        private readonly ConcurrentBag<ArticleAggregate> _articles;

        public InMemoryArticleRepository(ConcurrentBag<ArticleAggregate> articles)
        {
            _articles = articles;
        }

        public IQueryable<ArticleAggregate> Query()
        {
            return _articles.AsQueryable();
        }


        public Task Add(ArticleAggregate article, CancellationToken cancellationToken)
        {
            _articles.Add(article);
            return Task.CompletedTask;
        }
        public Task<int> SaveChanges(CancellationToken cancellationToken)
        {
            return Task.FromResult(1);
        }
    }
}
