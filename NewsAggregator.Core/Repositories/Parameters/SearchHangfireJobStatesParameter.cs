namespace NewsAggregator.Core.Repositories.Parameters
{
    public class SearchHangfireJobStatesParameter
    {
        public int StartIndex { get; set; }
        public int Count { get; set; }
        public long JobId { get; set; }
    }
}
