using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.ML.Articles
{
    public interface IArticleManager
    {
        Task TrainArticles(IEnumerable<string> languages, CancellationToken cancellationToken);
    }
}
