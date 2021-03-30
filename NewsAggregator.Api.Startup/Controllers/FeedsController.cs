using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAggregator.Api.Feeds.Queries;
using System.Security.Claims;
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

        [HttpGet("me")]
        [Authorize("Authenticated")]
        public async Task<IActionResult> GetMyFeeds()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await _mediator.Send(new GetMyFeedsQuery(userId));
            return new OkObjectResult(result);
        }
    }
}
