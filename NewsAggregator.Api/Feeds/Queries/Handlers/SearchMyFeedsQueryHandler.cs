using MediatR;
using NewsAggregator.Core.QueryResults;
using NewsAggregator.Core.Repositories;
using NewsAggregator.Core.Repositories.Parameters;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.Feeds.Queries.Handlers
{
    public class SearchMyFeedsQueryHandler : IRequestHandler<SearchMyFeedsQuery, SearchQueryResult<DetailedFeedQueryResult>>
    {
        private readonly IFeedQueryRepository _feedQueryRepository;

        public SearchMyFeedsQueryHandler(IFeedQueryRepository feedQueryRepository)
        {
            _feedQueryRepository = feedQueryRepository;
        }

        public Task<SearchQueryResult<DetailedFeedQueryResult>> Handle(SearchMyFeedsQuery request, CancellationToken cancellationToken)
        {
            return _feedQueryRepository.SearchFeeds(new SearchFeedParameter
            {
                DatasourceIds = request.DatasourceIds,
                FollowersFilter = Parse<FollowerFilterTypes>(request.FollowersFilter),
                IsPaginationEnabled = request.IsPaginationEnabled,
                StoriesFilter = Parse<NumberStoriesFilterTypes>(request.StoriesFitler),
                Count = request.Count,
                Direction = request.Direction,
                FeedTitle = request.FeedTitle,
                Order = request.Order,
                StartIndex = request.StartIndex,
                UserId = request.UserId
            }, cancellationToken);
        }

        private static T? Parse<T>(int? number) where T : struct
        {
            if (number == null)
            {
                return null;
            }

            if (Enum.IsDefined(typeof(T), number))
            {
                var name = Enum.GetNames(typeof(T)).ElementAt(number.Value);
                return (T)Enum.Parse(typeof(T), name);
            }

            return null;
        }
    }
}
