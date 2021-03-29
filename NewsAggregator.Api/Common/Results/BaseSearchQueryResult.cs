using System.Collections.Generic;

namespace NewsAggregator.Api.Common.Results
{
    public class BaseSearchQueryResult<T> where T : class
    {
        public BaseSearchQueryResult()
        {
            Content = new List<T>();
        }

        public int Count { get; set; }
        public int StartIndex { get; set; }
        public ICollection<T> Content { get; set; }
    }
}
