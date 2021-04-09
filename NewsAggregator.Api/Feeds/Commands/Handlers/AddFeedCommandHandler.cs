using MediatR;
using Microsoft.Extensions.Logging;
using NewsAggregator.Api.DataSources;
using NewsAggregator.Api.Resources;
using NewsAggregator.Core.Domains.Feeds;
using NewsAggregator.Core.Exceptions;
using NewsAggregator.Core.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.Feeds.Commands.Handlers
{
    public class AddFeedCommandHandler : IRequestHandler<AddFeedCommand, string>
    {
        private readonly IFeedCommandRepository _feedCommandRepository;
        private readonly IDataSourceService _dataSourceService;
        private readonly ILogger<AddFeedCommandHandler> _logger;

        public AddFeedCommandHandler(
            IFeedCommandRepository feedCommandRepository,
            IDataSourceService dataSourceService,
            ILogger<AddFeedCommandHandler> logger)
        {
            _feedCommandRepository = feedCommandRepository;
            _dataSourceService = dataSourceService;
            _logger = logger;
        }

        public async Task<string> Handle(AddFeedCommand request, CancellationToken cancellationToken)
        {
            var newFeed = FeedAggregate.Create(request.UserId, request.Title);
            if (request.DatasourceIds != null)
            {
                var datasources = await _dataSourceService.Get(request.DatasourceIds, cancellationToken);
                var missingDatasources = request.DatasourceIds.Where(d => !datasources.Any(ds => ds.Id == d));
                if (missingDatasources.Any())
                {
                    throw new NewsAggregatorException(string.Format(Global.UnknownDataSource, string.Join(",", missingDatasources)));
                }

                foreach(var datasource in datasources)
                {
                    newFeed.SubscribeDataSource(request.UserId, datasource.Id);
                }
            }

            var feed = await _feedCommandRepository.Get(request.UserId, request.Title, cancellationToken);
            if (feed != null)
            {
                _logger.LogError($"Feed with the title {request.Title} already exists");
                throw new NewsAggregatorException(string.Format(Global.FeedAlreadyExists, request.Title));
            }

            await _feedCommandRepository.Add(newFeed, cancellationToken);
            await _feedCommandRepository.SaveChanges(cancellationToken);
            _logger.LogInformation($"User {request.UserId} adds the feed {request.Title}");
            return newFeed.Id;
        }
    }
}
