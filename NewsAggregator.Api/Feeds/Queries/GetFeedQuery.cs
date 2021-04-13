using MediatR;
using NewsAggregator.Core.QueryResults;

namespace NewsAggregator.Api.Feeds.Queries
{
    public class GetFeedQuery : IRequest<FeedQueryResult>
    {
        public GetFeedQuery(string feedId)
        {
            FeedId = feedId;
        }

        public string FeedId { get; set; }
    }
}
