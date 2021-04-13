using MediatR;
using NewsAggregator.Core.QueryResults;
using System.Collections.Generic;

namespace NewsAggregator.Api.Feeds.Queries
{
    public class GetMyFeedsQuery : IRequest<IEnumerable<DetailedFeedQueryResult>>
    {
        public GetMyFeedsQuery(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; set; }
    }
}
