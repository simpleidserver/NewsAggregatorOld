using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.ML.Jobs
{
    public interface INextArticleRecommenderJob
    {
        Task Run(CancellationToken cancellationToken);
    }
}
