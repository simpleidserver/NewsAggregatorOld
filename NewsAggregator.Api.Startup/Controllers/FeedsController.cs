using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAggregator.Api.Feeds.Commands;
using NewsAggregator.Api.Feeds.Queries;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.Startup.Controllers
{
    [Route("feeds")]
    public class FeedsController : Controller
    {
        private readonly IMediator _mediator;

        public FeedsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize("Authenticated")]
        public async Task<IActionResult> AddFeed([FromBody] AddFeedCommand addFeedCommand, CancellationToken cancellationToken)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            addFeedCommand.UserId = userId;
            var result = await _mediator.Send(addFeedCommand, cancellationToken);
            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.Created,
                Content = new JObject
                {
                    { "id", result }
                }.ToString(),
                ContentType = "application/json"
            };
        }


        [HttpGet("{feedId}")]
        [Authorize("Authenticated")]
        public async Task<IActionResult> GetFeed(string feedId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetFeedQuery(feedId), cancellationToken);
            return new OkObjectResult(result);
        }


        [HttpDelete("{feedId}")]
        [Authorize("Authenticated")]
        public async Task<IActionResult> DeleteFeed(string feedId, CancellationToken cancellationToken)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await _mediator.Send(new DeleteFeedCommand { FeedId = feedId, UserId = userId }, cancellationToken);
            return new NoContentResult();
        }

        [HttpGet("me")]
        [Authorize("Authenticated")]
        public async Task<IActionResult> GetMyFeeds(CancellationToken cancellationToken)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await _mediator.Send(new GetMyFeedsQuery(userId));
            return new OkObjectResult(result);
        }

        [HttpPost("me/.search")]
        [Authorize("Authenticated")]
        public async Task<IActionResult> SearchMyFeeds([FromBody] SearchMyFeedsQuery parameter, CancellationToken cancellationToken)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            parameter.UserId = userId;
            var result = await _mediator.Send(parameter, cancellationToken);
            return new OkObjectResult(result);
        }

        [HttpPost("{id}/datasources")]
        public async Task<IActionResult> Subscribe(string id, [FromBody] SubscribeDatasourceCommand cmd, CancellationToken cancellationToken)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            cmd.FeedId = id;
            cmd.UserId = userId;
            await _mediator.Send(cmd, cancellationToken);
            return new NoContentResult();
        }

        [HttpDelete("{id}/datasources/{datasourceId}")]
        public async Task<IActionResult> UnSubscribe(string id, string datasourceId, CancellationToken cancellationToken)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var cmd = new UnSubscribeDatasourceCommand { UserId = userId, DatasourceId = datasourceId, FeedId = id };
            await _mediator.Send(cmd, cancellationToken);
            return new NoContentResult();
        }
    }
}
