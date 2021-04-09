using MediatR;
using NewsAggregator.Core.QueryResults;
using System.Collections.Generic;

namespace NewsAggregator.Api.Feeds.Commands
{
    public class AddFeedCommand : IRequest<string>
    {
        public string Title { get; set; }
        public IEnumerable<string> DatasourceIds { get; set; }
        public string UserId { get; set; }
    }
}
