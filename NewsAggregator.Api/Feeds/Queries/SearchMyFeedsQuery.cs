using MediatR;
using NewsAggregator.Core.QueryResults;
using System.Collections.Generic;

namespace NewsAggregator.Api.Feeds.Queries
{
    public class SearchMyFeedsQuery : IRequest<SearchQueryResult<DetailedFeedQueryResult>>
    {
        public bool IsPaginationEnabled { get; set; }
        public int? StartIndex { get; set; }
        public int? Count { get; set; }
        public string Order { get; set; }
        public string Direction { get; set; }
        public string FeedTitle { get; set; }
        public IEnumerable<string> DatasourceIds { get; set; }
        public int? FollowersFilter { get; set; }
        public int? StoriesFitler { get; set; }
        public string UserId { get; set; }
    }
}