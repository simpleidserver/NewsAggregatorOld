using MediatR;
using Microsoft.Extensions.Logging;
using NewsAggregator.Api.Feeds;
using NewsAggregator.Api.Resources;
using NewsAggregator.Core.Exceptions;
using NewsAggregator.Core.QueryResults;
using NewsAggregator.Core.Repositories;
using NewsAggregator.Core.Repositories.Parameters;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.Articles.Queries.Handlers
{
    public class SearchArticlesInFeedQueryHandler : IRequestHandler<SearchArticlesInFeedQuery, SearchQueryResult<ArticleQueryResult>>
    {
        private readonly IFeedService _feedService;
        private readonly IArticleQueryRepository _articleQueryRepository;
        private readonly ILogger<SearchArticlesInFeedQueryHandler> _logger;

        public SearchArticlesInFeedQueryHandler(
            IFeedService feedService,
            IArticleQueryRepository articleQueryRepository,
            ILogger<SearchArticlesInFeedQueryHandler> logger)
        {
            _feedService = feedService;
            _articleQueryRepository = articleQueryRepository;
            _logger = logger;
        }

        public async Task<SearchQueryResult<ArticleQueryResult>> Handle(SearchArticlesInFeedQuery request, CancellationToken cancellationToken)
        {
            var feed = await _feedService.Get(request.FeedId, cancellationToken);
            if (feed.UserId != request.UserId) 
            {
                _logger.LogError($"the user {request.UserId} doesn't have the right to access to the feed {request.FeedId}");
                throw new NewsAggregatorUnauthorizedException(string.Format(Global.CannotAccessToTheFeed, request.FeedId));
            }

            return await _articleQueryRepository.SearchInFeed(new SearchArticlesInFeedParameter
            {
                Count = request.Count,
                Direction = request.Direction,
                FeedId = request.FeedId,
                Order = request.Order,
                StartIndex = request.StartIndex,
                UserId = request.UserId
            }, cancellationToken);
        }
    }
}
