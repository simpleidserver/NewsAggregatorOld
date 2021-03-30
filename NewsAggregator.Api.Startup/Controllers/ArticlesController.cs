using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NewsAggregator.Api.Startup.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IMediator _mediator;

        public ArticlesController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
