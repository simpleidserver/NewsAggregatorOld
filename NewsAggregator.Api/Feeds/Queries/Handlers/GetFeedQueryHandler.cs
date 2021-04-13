using MediatR;
using Microsoft.Extensions.Logging;
using NewsAggregator.Api.Resources;
using NewsAggregator.Core.Exceptions;
using NewsAggregator.Core.QueryResults;
using NewsAggregator.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.Feeds.Queries.Handlers
{
    public class GetFeedQueryHandler : IRequestHandler<GetFeedQuery, FeedQueryResult>
    {
        private readonly IFeedQueryRepository _feedQueryRepository;
        private readonly ILogger<GetFeedQueryHandler> _logger;

        public GetFeedQueryHandler(
            IFeedQueryRepository feedQueryRepository,
            ILogger<GetFeedQueryHandler> logger)
        {
            _feedQueryRepository = feedQueryRepository;
            _logger = logger;
        }

        public async Task<FeedQueryResult> Handle(GetFeedQuery request, CancellationToken cancellationToken)
        {
            var feed = await _feedQueryRepository.Get(request.FeedId, cancellationToken);
            if (feed == null)
            {
                _logger.LogError($"feed {request.FeedId} doesn't exist");
                throw new NewsAggregatorResourceNotFoundException(string.Format(Global.FeedDoesntExist, request.FeedId));
            }

            return feed;
        }
    }
}
