namespace NewsAggregator.Domain.Feeds
{
    public class FeedDatasource
    {
        private FeedDatasource() { }

        public string DatasourceId { get; set; }

        public static FeedDatasource Create(string datasourceId)
        {
            var result = new FeedDatasource { };
            return result;
        }
    }
}
