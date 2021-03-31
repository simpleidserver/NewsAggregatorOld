using MediatR;
using Microsoft.Extensions.Logging;
using NewsAggregator.Api.Resources;
using NewsAggregator.Common.Exceptions;
using NewsAggregator.Core.Repositories;
using NewsAggregator.Domain.Feeds;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.Feeds.Commands.Handlers
{
    public class AddFeedCommandHandler : IRequestHandler<AddFeedCommand, string>
    {
        private readonly IFeedCommandRepository _feedCommandRepository;
        private readonly ILogger<AddFeedCommandHandler> _logger;

        public AddFeedCommandHandler(
            IFeedCommandRepository feedCommandRepository,
            ILogger<AddFeedCommandHandler> logger)
        {
            _feedCommandRepository = feedCommandRepository;
            _logger = logger;
        }

        public async Task<string> Handle(AddFeedCommand request, CancellationToken cancellationToken)
        {
            var newFeed = FeedAggregate.Create(request.UserId, request.Title);
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
