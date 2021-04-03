using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAggregator.Api.Recommendations.Queries;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.Startup.Controllers
{
    [Route("recommendations")]
    public class RecommendationsController : Controller
    {
        private readonly IMediator _mediator;

        public RecommendationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(".search")]
        [Authorize("Authenticated")]
        public async Task<IActionResult> Search([FromBody] SearchRecommendationsQuery query, CancellationToken cancellationToken)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            query.UserId = userId;
            var result = await _mediator.Send(query);
            return new OkObjectResult(result);
        }
    }
}
