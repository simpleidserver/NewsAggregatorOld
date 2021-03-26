using System.Threading.Tasks;

namespace NewsAggregator.ML.Infrastructures.Jobs
{
    public interface IJob
    {
        Task Start();
        Task Stop();
    }
}
