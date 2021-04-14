using System;

namespace NewsAggregator.Core.QueryResults
{
    public class HangfireJobStateQueryResult
    {
        public string Name { get; set; }
        public string Reason { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Data { get; set; }
    }
}
