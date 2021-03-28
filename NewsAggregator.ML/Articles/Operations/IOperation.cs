using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.ML.Articles.Operations
{
    public interface IOperation
    {
        Task Execute(CancellationToken cancellationToken);
        void Rollback();
    }
}
