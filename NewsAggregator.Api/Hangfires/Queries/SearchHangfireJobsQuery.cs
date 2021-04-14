using MediatR;
using NewsAggregator.Core.QueryResults;

namespace NewsAggregator.Api.Hangfires.Queries
{
    public class SearchHangfireJobsQuery : IRequest<SearchQueryResult<HangfireJobQueryResult>>
    {
        public int StartIndex { get; set; }
        public int Count { get; set; }
    }
}
