using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewsAggregator.Api.DataSources.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.Startup.Controllers
{
    [Route("datasources")]
    public class DatasourcesController : Controller
    {
        private readonly IMediator _mediator;

        public DatasourcesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(".search")]
        public async Task<IActionResult> Search([FromBody] SearchDataSourcesQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return new OkObjectResult(result);
        }
    }
}
