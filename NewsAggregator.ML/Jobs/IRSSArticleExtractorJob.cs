using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.ML.Jobs
{
    public interface IRSSArticleExtractorJob
    {
        Task Run(CancellationToken cancellationToken);
    }
}
