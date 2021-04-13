namespace NewsAggregator.Core.QueryResults
{
    public class DetailedFeedQueryResult
    {
        public string FeedId { get; set; }
        public string FeedTitle { get; set; }
        public string DatasourceId { get; set; }
        public string DatasourceTitle { get; set; }
        public string DatasourceDescription { get; set; }
        public int NbFollowers { get; set; }
        public int NbStoriesPerMonth { get; set; }
        public string Language { get; set; }
    }
}
