using NewsAggregator.Core.Domains.Recommendations;
using NewsAggregator.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.EF.Repositories
{
    public class RecommendationCommandRepository : IRecommendationCommandRepository
    {
        private readonly NewsAggregatorDBContext _newsAggregatorDBContext;

        public RecommendationCommandRepository(NewsAggregatorDBContext newsAggregatorDBContext)
        {
            _newsAggregatorDBContext = newsAggregatorDBContext;
        }

        public Task Add(RecommendationAggregate recommendation, CancellationToken cancellationToken)
        {
            _newsAggregatorDBContext.Recommendations.Add(recommendation);
            return Task.CompletedTask;
        }

        public Task Update(RecommendationAggregate recommendation, CancellationToken cancellationToken)
        {
            _newsAggregatorDBContext.Recommendations.Update(recommendation);
            return Task.CompletedTask;
        }

        public Task<int> SaveChanges(CancellationToken cancellationToken)
        {
            return _newsAggregatorDBContext.SaveChangesAsync(cancellationToken);
        }
    }
}
