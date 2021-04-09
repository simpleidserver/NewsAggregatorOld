using MediatR;

namespace NewsAggregator.Api.Feeds.Commands
{
    public class DeleteFeedCommand: IRequest<bool>
    {
        public string FeedId { get; set; }
        public string UserId { get; set; }
    }
}
