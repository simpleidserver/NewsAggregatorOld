using System.Collections.Generic;

namespace NewsAggregator.Core.Repositories.Parameters
{
    public class SearchFeedParameter
    {
        public bool IsPaginationEnabled { get; set; }
        public int? StartIndex { get; set; }
        public int? Count { get; set; }
        public string Order { get; set; }
        public string Direction { get; set; }
        public string FeedTitle { get; set; }
        public IEnumerable<string> DatasourceIds { get; set; }
        public FollowerFilterTypes? FollowersFilter { get; set; }
        public NumberStoriesFilterTypes? StoriesFilter { get; set; }
        public string UserId { get; set; }
    }
}
