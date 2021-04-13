using MediatR;
using NewsAggregator.Api.Feeds.Queries;
using NewsAggregator.Core.QueryResults;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.Feeds
{
    public class FeedService : IFeedService
    {
        private readonly IMediator _mediator;

        public FeedService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<FeedQueryResult> Get(string id, CancellationToken cancellationToken)
        {
            return _mediator.Send(new GetFeedQuery(id), cancellationToken);
        }
    }
}
