using MediatR;
using NewsAggregator.Core.QueryResults;

namespace NewsAggregator.Api.Hangfires.Queries
{
    public class SearchHangfireJobStatesQuery: IRequest<SearchQueryResult<HangfireJobStateQueryResult>>
    {
        public int StartIndex { get; set; }
        public int Count { get; set; }
        public long JobId { get; set; }
    }
}
