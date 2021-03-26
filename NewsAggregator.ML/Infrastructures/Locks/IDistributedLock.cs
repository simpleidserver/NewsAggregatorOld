using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.ML.Infrastructures.Locks
{
    public interface IDistributedLock
    {
        Task<bool> TryAcquireLock(string id, CancellationToken token);
        Task ReleaseLock(string id, CancellationToken token);
    }
}
