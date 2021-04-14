using MediatR;
using Microsoft.Extensions.Logging;
using NewsAggregator.Api.Resources;
using NewsAggregator.Core.Exceptions;
using NewsAggregator.Core.QueryResults;
using NewsAggregator.Core.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.DataSources.Queries.Handlers
{
    public class GetDataSourceQueryHandler : IRequestHandler<GetDataSourceQuery, DataSourceQueryResult>
    {
        private readonly ILogger<GetDataSourceQueryHandler> _logger;
        private readonly IDataSourceQueryRepository _dataSourceQueryRepository;

        public GetDataSourceQueryHandler(
            ILogger<GetDataSourceQueryHandler> logger,
            IDataSourceQueryRepository dataSourceQueryRepository)
        {
            _logger = logger;
            _dataSourceQueryRepository = dataSourceQueryRepository;
        }

        public async Task<DataSourceQueryResult> Handle(GetDataSourceQuery request, CancellationToken cancellationToken)
        {
            var datasource = await _dataSourceQueryRepository.Get(new string[] { request.DataSourceId }, cancellationToken);
            if (!datasource.Any())
            {
                _logger.LogError($"the datasource {request.DataSourceId} doesn't exist");
                throw new NewsAggregatorResourceNotFoundException(string.Format(Global.DataSourceDoesntExist, request.DataSourceId));
            }

            return datasource.First();
        }
    }
}
