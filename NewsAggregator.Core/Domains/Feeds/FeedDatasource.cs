
namespace NewsAggregator.Core.Domains.Feeds
{
    public class FeedDatasource
    {
        private FeedDatasource() { }

        public string DatasourceId { get; set; }

        public static FeedDatasource Create(string datasourceId)
        {
            var result = new FeedDatasource { DatasourceId = datasourceId };
            return result;
        }
    }
}
