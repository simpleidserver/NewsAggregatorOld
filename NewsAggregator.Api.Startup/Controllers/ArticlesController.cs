using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize("Authenticated")]
        public async Task<IActionResult> SearchInDatasource(string datasourceId, [FromBody] SearchArticlesInDataSourceQuery searchArticlesInFeedQuery, CancellationToken cancellationToken)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            searchArticlesInFeedQuery.UserId = userId;
            searchArticlesInFeedQuery.DataSourceId = datasourceId;
            var result = await _mediator.Send(searchArticlesInFeedQuery, cancellationToken);
            return new OkObjectResult(result);
        }

        [HttpPost("searchinfeed/{feedId}")]
        [Authorize("Authenticated")]
        public async Task<IActionResult> SearchInFeed(string feedId, [FromBody] SearchArticlesInFeedQuery searchArticlesInFeedQuery, CancellationToken cancellationToken)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            searchArticlesInFeedQuery.UserId = userId;
            searchArticlesInFeedQuery.FeedId = feedId;
            var result = await _mediator.Send(searchArticlesInFeedQuery, cancellationToken);
            return new OkObjectResult(result);
        }

        [HttpGet("{articleId}/read")]
        [Authorize("Authenticated")]
        public async Task<IActionResult> Read(string articleId, CancellationToken cancellationToken)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var nonce = User.FindFirst("nonce").Value;
            var cmd = new ReadArticleCommand {ArticleId = articleId, SessionId = nonce, UserId = userId };
            await _mediator.Send(cmd, cancellationToken);
            return new NoContentResult();
        }

        [HttpGet("{articleId}/unread")]
        [Authorize("Authenticated")]
        public async Task<IActionResult> Unread(string articleId, CancellationToken cancellationToken)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var nonce = User.FindFirst("nonce").Value;
            var cmd = new UnreadArticleCommand { ArticleId = articleId, SessionId = nonce, UserId = userId };
            await _mediator.Send(cmd, cancellationToken);
            return new NoContentResult();
        }

        [HttpGet("{articleId}/readAndHide")]
        public async Task<IActionResult> ReadAndHide(string articleId, CancellationToken cancellationToken)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var nonce = User.FindFirst("nonce").Value;
            var cmd = new ReadAndHideArticleCommand { ArticleId = articleId, SessionId = nonce, UserId = userId };
            await _mediator.Send(cmd, cancellationToken);
            return new NoContentResult();
        }

        [HttpGet("{articleId}/like")]
        [Authorize("Authenticated")]
        public async Task<IActionResult> Like(string articleId, CancellationToken cancellationToken)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var nonce = User.FindFirst("nonce").Value;
            var cmd = new LikeArticleCommand { ArticleId = articleId, SessionId = nonce, UserId = userId };
            await _mediator.Send(cmd, cancellationToken);
            return new NoContentResult();
        }

        [HttpGet("{articleId}/unlike")]
        [Authorize("Authenticated")]
        public async Task<IActionResult> Unlike(string articleId, CancellationToken cancellationToken)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var nonce = User.FindFirst("nonce").Value;
            var cmd = new UnlikeArticleCommand { ArticleId = articleId, SessionId = nonce, UserId = userId };
            await _mediator.Send(cmd, cancellationToken);
            return new NoContentResult();
        }
    }
}
