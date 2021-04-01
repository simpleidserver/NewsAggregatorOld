using NewsAggregator.Core.Domains.Recommendations;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Core.Repositories
{
    public interface IRecommendationCommandRepository
    {
        Task Add(RecommendationAggregate recommendation, CancellationToken cancellationToken);
        Task Update(RecommendationAggregate recommendation, CancellationToken cancellationToken);
        Task<int> SaveChanges(CancellationToken cancellationToken);
    }
}
