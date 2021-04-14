using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAggregator.Api.Hangfires.Queries;
using NewsAggregator.Core.Repositories.Parameters;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.Startup.Controllers
{
    [Route("hangfire")]
    public class HangfireController
    {
        private readonly IMediator _mediator;

        public HangfireController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("extract-articles")]
        [Authorize("IsAdmin")]
        public IActionResult ExtractArticles()
        {
            RecurringJob.Trigger("rssExtractArticles");
            return new NoContentResult();
        }

        [HttpGet("extract-recommendations")]
        [Authorize("IsAdmin")]
        public IActionResult ExtractRecommendations()
        {
            RecurringJob.Trigger("nextArticlesRecommender");
            return new NoContentResult();
        }

        [HttpPost("searchjobs")]
        [Authorize("IsAdmin")]
        public async Task<IActionResult> SearchHangfireJobs([FromBody] SearchHangfireJobsQuery parameter, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(parameter, cancellationToken);
            return new OkObjectResult(result);
        }

        [HttpPost("searchjobstates")]
        [Authorize("IsAdmin")]
        public async Task<IActionResult> SearchHangfireJobStates([FromBody] SearchHangfireJobStatesQuery parameter, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(parameter, cancellationToken);
            return new OkObjectResult(result);
        }
    }
}
