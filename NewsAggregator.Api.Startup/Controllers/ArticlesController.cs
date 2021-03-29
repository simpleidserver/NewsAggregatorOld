using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewsAggregator.Api.Articles.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.Startup.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IMediator _mediator;

        public ArticlesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(".search")]
        public async Task<IActionResult> Search([FromBody] SearchArticlesQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return new OkObjectResult(result);
        }
    }
}
