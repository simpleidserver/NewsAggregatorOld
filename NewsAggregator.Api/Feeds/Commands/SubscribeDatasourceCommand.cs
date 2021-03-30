using MediatR;

namespace NewsAggregator.Api.Feeds.Commands
{
    public class SubscribeDatasourceCommand : IRequest<bool>
    {
        public string FeedId { get; set; }
        public string DatasourceId { get; set; }
        public string UserId { get; set; }
    }
}
