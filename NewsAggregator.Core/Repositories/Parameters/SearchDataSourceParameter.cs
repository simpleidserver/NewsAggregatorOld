namespace NewsAggregator.Core.Repositories.Parameters
{
    public class SearchDataSourceParameter
    {
        public int StartIndex { get; set; }
        public int Count { get; set; }
        public string Title { get; set; }
        public bool IsPaginationEnabled { get; set; }
    }
}
