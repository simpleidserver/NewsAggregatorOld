using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NewsAggregator.ML.Infrastructures.Locks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.ML.Infrastructures.Jobs
{
    public abstract class BaseScheduledJob: IJob
    {
        public BaseScheduledJob(
            IDistributedLock distributedLock, 
            IOptions<NewsAggregatorMLOptions> options, 
            ILogger<BaseScheduledJob> logger)
        {
            DistributedLock = distributedLock;
            Options = options.Value;
            Logger = logger;
        }

        protected IDistributedLock DistributedLock { get; private set; }
        protected NewsAggregatorMLOptions Options { get; private set; }
        protected ILogger<BaseScheduledJob> Logger { get; private set; }
        protected CancellationTokenSource CancellationTokenSource { get; private set; }
        protected Task CurrentTask { get; private set; }
        protected DateTime? NextExecutionDateTime { get; private set; }
        protected abstract string LockName { get; }
        protected abstract int IntervalMS { get; }

        public Task Start()
        {
            CancellationTokenSource = new CancellationTokenSource();
            CurrentTask = new Task(Handle, TaskCreationOptions.LongRunning);
            CurrentTask.Start();
            return Task.CompletedTask;
        }

        public Task Stop()
        {
            CancellationTokenSource.Cancel();
            return CurrentTask;
        }

        private async void Handle()
        {
            var cancellationToken = CancellationTokenSource.Token;
            while (!CancellationTokenSource.IsCancellationRequested)
            {
                Thread.Sleep(Options.BlockThreadMS);
                if (NextExecutionDateTime != null && DateTime.UtcNow <= NextExecutionDateTime.Value)
                {
                    continue;
                }

                if (!await DistributedLock.TryAcquireLock(LockName, cancellationToken))
                {
                    continue;
                }

                try
                {
                    await Execute(cancellationToken);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex.ToString());
                }
                finally
                {
                    await DistributedLock.ReleaseLock(LockName, cancellationToken);
                    NextExecutionDateTime = DateTime.UtcNow.AddMilliseconds(IntervalMS);
                }
            }
        }

        protected abstract Task Execute(CancellationToken token);

    }
}