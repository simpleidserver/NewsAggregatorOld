using NewsAggregator.ML.Infrastructures.Jobs;
using System.Collections.Generic;

namespace NewsAggregator.ML
{
    public class MLJobServer : IMLJobServer
    {
        private readonly IEnumerable<IJob> _jobs;

        public MLJobServer(IEnumerable<IJob> jobs)
        {
            _jobs = jobs;
        }

        public async void Start()
        {
            foreach (var job in _jobs)
            {
                await job.Start();
            }
        }

        public async void Stop()
        {
            foreach (var job in _jobs)
            {
                await job.Stop();
            }
        }
    }
}
