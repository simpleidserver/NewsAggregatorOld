using MediatR;
using NewsAggregator.Core.QueryResults;
using NewsAggregator.Core.Repositories;
using NewsAggregator.Core.Repositories.Parameters;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.Hangfires.Queries.Handlers
{
    public class SearchHangfireJobStatesQueryHandler : IRequestHandler<SearchHangfireJobStatesQuery, SearchQueryResult<HangfireJobStateQueryResult>>
    {
        private readonly IHangfireQueryRepository _hangfireQueryRepository;

        public SearchHangfireJobStatesQueryHandler(IHangfireQueryRepository hangfireQueryRepository)
        {
            _hangfireQueryRepository = hangfireQueryRepository;
        }

        public Task<SearchQueryResult<HangfireJobStateQueryResult>> Handle(SearchHangfireJobStatesQuery request, CancellationToken cancellationToken)
        {
            return _hangfireQueryRepository.SearchStates(new SearchHangfireJobStatesParameter
            {
                Count = request.Count,
                StartIndex = request.StartIndex,
                JobId = request.JobId
            }, cancellationToken);
        }
    }
}
