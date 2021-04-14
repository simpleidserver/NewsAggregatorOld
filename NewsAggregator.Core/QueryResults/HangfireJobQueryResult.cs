using System;

namespace NewsAggregator.Core.QueryResults
{
    public class HangfireJobQueryResult
    {
        public long Id { get; set; }
        public string InvocationData { get; set; }
        public string StateName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
