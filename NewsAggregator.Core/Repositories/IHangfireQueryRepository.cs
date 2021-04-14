using NewsAggregator.Core.QueryResults;
using NewsAggregator.Core.Repositories.Parameters;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Core.Repositories
{
    public interface IHangfireQueryRepository
    {
        Task<SearchQueryResult<HangfireJobQueryResult>> SearchJobs(SearchHangfireJobsParameter parameter, CancellationToken cancellationToken);
        Task<SearchQueryResult<HangfireJobStateQueryResult>> SearchStates(SearchHangfireJobStatesParameter parameter, CancellationToken cancellationToken);
    }
}
