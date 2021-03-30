using MediatR;

namespace NewsAggregator.Api.Feeds.Commands
{
    public class AddFeedCommand : IRequest<string>
    {
        public string Title { get; set; }
        public string UserId { get; set; }
    }
}
