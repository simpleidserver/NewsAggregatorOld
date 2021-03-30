using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.ML.Jobs
{
    public interface IArticleExtractorJob
    {
        Task Run(CancellationToken cancellationToken);
    }
}
