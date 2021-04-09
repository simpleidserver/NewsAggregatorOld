using System.Collections.Generic;

namespace NewsAggregator.Core.QueryResults
{
    public class SearchQueryResult<T> where T : class
    {
        public int? StartIndex { get; set; }
        public int? Count { get; set; }
        public IEnumerable<T> Content { get; set; }
    }
}
