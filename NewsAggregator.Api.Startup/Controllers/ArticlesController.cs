using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewsAggregator.Api.Articles.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.Startup.Controllers
{
    [Route("articles")]
    public class ArticlesController : Controller
    {
        private readonly IMediator _mediator;

        public ArticlesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("searchindatasource/{datasourceId}")]
        public async Task<IActionResult> SeachInFeeds(string datasourceId, [FromBody] SearchArticlesInFeedQuery searchArticlesInFeedQuery, CancellationToken cancellationToken)
        {
            searchArticlesInFeedQuery.DataSourceId = datasourceId;
            var result = await _mediator.Send(searchArticlesInFeedQuery, cancellationToken);
            return new OkObjectResult(result);
        }
    }
}
