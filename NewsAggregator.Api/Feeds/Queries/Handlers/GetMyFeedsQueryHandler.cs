using MediatR;
using NewsAggregator.Core.QueryResults;
using NewsAggregator.Core.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.Feeds.Queries.Handlers
{
    public class GetMyFeedsQueryHandler : IRequestHandler<GetMyFeedsQuery, IEnumerable<DetailedFeedQueryResult>>
    {
        private readonly IFeedQueryRepository _feedQueryRepository;

        public GetMyFeedsQueryHandler(IFeedQueryRepository feedQueryRepository)
        {
            _feedQueryRepository = feedQueryRepository;
        }

        public Task<IEnumerable<DetailedFeedQueryResult>> Handle(GetMyFeedsQuery request, CancellationToken cancellationToken)
        {
            return _feedQueryRepository.GetFeeds(request.UserId);
        }
    }
}
