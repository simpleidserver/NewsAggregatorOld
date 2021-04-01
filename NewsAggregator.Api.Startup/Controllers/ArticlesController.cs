using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewsAggregator.Api.Articles.Commands;
using NewsAggregator.Api.Articles.Queries;
using System.Security.Claims;
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

        [HttpGet("{articleId}/view")]
        public async Task<IActionResult> View(string articleId, CancellationToken cancellationToken)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var nonce = User.FindFirst("nonce").Value;
            var cmd = new ViewArticleCommand {ArticleId = articleId, SessionId = nonce, UserId = userId };
            await _mediator.Send(cmd, cancellationToken);
            return new NoContentResult();
        }

        [HttpGet("{articleId}/like")]
        public async Task<IActionResult> Like(string articleId, CancellationToken cancellationToken)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var nonce = User.FindFirst("nonce").Value;
            var cmd = new LikeArticleCommand { ArticleId = articleId, SessionId = nonce, UserId = userId };
            await _mediator.Send(cmd, cancellationToken);
            return new NoContentResult();
        }
    }
}
