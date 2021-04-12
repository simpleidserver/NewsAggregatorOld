using MediatR;
using NewsAggregator.Core.QueryResults;

namespace NewsAggregator.Api.DataSources.Queries
{
    public class SearchDataSourcesQuery : IRequest<SearchQueryResult<DataSourceQueryResult>>
    {
        public int StartIndex { get; set; }
        public int Count { get; set; }
        public string Title { get; set; }
        public bool IsPaginationEnabled { get; set; }
    }
}
