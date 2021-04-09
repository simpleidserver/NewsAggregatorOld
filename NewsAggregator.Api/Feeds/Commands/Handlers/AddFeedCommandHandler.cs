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
            var isNewFeed = false;
            var feed = await _feedCommandRepository.Get(request.UserId, request.Title, cancellationToken);
            if (feed == null)
            {
                feed = FeedAggregate.Create(request.UserId, request.Title);
                isNewFeed = true;
            }

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
                    feed.SubscribeDataSource(request.UserId, datasource.Id);
                }
            }

            if (isNewFeed)
            {
                await _feedCommandRepository.Add(feed, cancellationToken);
            }
            else
            {
                await _feedCommandRepository.Update(feed, cancellationToken);
            }

            await _feedCommandRepository.SaveChanges(cancellationToken);
            _logger.LogInformation($"User {request.UserId} adds the feed {request.Title}");
            return feed.Id;
        }
    }
}
