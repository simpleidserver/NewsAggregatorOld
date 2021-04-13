namespace NewsAggregator.Core.Repositories.Parameters
{
    public class SearchArticlesInDataSourceParameter
    {
        public int StartIndex { get; set; }
        public int Count { get; set; }
        public string Order { get; set; }
        public string Direction { get; set; }
        public string DataSourceId { get; set; }
        public string UserId { get; set; }
    }
}
