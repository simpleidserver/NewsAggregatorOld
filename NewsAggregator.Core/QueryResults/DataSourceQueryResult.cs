namespace NewsAggregator.Core.QueryResults
{
    public class DataSourceQueryResult
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int NbStoriesPerMonth { get; set; }
        public int NbFollowers { get; set; }
        public string Url { get; set; }
    }
}
