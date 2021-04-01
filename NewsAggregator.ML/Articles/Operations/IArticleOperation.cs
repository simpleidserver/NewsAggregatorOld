using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.ML.Articles.Operations
{
    public interface IArticleOperation
    {
        Task Execute(string language, CancellationToken cancellationToken);
    }
}
