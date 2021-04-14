using MediatR;
using NewsAggregator.Core.QueryResults;
using NewsAggregator.Core.Repositories;
using NewsAggregator.Core.Repositories.Parameters;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.Hangfires.Queries.Handlers
{
    public class SearchHangfireJobsQueryHandler : IRequestHandler<SearchHangfireJobsQuery, SearchQueryResult<HangfireJobQueryResult>>
    {
        private readonly IHangfireQueryRepository _hangfireQueryRepository;

        public SearchHangfireJobsQueryHandler(IHangfireQueryRepository hangfireQueryRepository)
        {
            _hangfireQueryRepository = hangfireQueryRepository;
        }

        public Task<SearchQueryResult<HangfireJobQueryResult>> Handle(SearchHangfireJobsQuery request, CancellationToken cancellationToken)
        {
            return _hangfireQueryRepository.SearchJobs(new SearchHangfireJobsParameter
            {
                Count = request.Count,
                StartIndex = request.StartIndex
            }, cancellationToken);
        }
    }
}
