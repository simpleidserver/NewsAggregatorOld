using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace NewsAggregator.Api.Startup.Controllers
{
    [Route("hangfire")]
    public class HangfireController
    {
        [HttpGet("extract-articles")]
        public IActionResult ExtractArticles()
        {
            RecurringJob.Trigger("rssExtractArticles");
            return new NoContentResult();
        }

        [HttpGet("extract-recommendations")]
        public IActionResult ExtractRecommendations()
        {
            RecurringJob.Trigger("nextArticlesRecommender");
            return new NoContentResult();
        }
    }
}
