using MassTransit;
using NewsAggregator.Core.Domains.Feeds.Events;
using NewsAggregator.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.ML.EventHandlers
{
    public class FeedDataSourceUnSubscribedEventConsumer : IConsumer<FeedDataSourceUnsubscribedEvent>
    {
        private readonly IDataSourceCommandRepository _datasourceCommandRepository;

        public FeedDataSourceUnSubscribedEventConsumer(IDataSourceCommandRepository dataSourceCommandRepository)
        {
            _datasourceCommandRepository = dataSourceCommandRepository;
        }

        public async Task Consume(ConsumeContext<FeedDataSourceUnsubscribedEvent> context)
        {
            var datasource = await _datasourceCommandRepository.Get(context.Message.DataSourceId, CancellationToken.None);
            datasource.DecrementFollower();
            await _datasourceCommandRepository.Update(new[] { datasource }, CancellationToken.None);
            await _datasourceCommandRepository.SaveChanges(CancellationToken.None);
        }
    }
}
