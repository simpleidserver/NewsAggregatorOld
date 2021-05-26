using MassTransit;
using NewsAggregator.Core.Domains.Feeds.Events;
using NewsAggregator.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.ML.EventHandlers
{
    public class FeedDataSourceSubscribedEventConsumer : IConsumer<FeedDataSourceSubscribedEvent>
    {
        private readonly IDataSourceCommandRepository _datasourceCommandRepository;

        public FeedDataSourceSubscribedEventConsumer(IDataSourceCommandRepository dataSourceCommandRepository)
        {
            _datasourceCommandRepository = dataSourceCommandRepository;
        }

        public async Task Consume(ConsumeContext<FeedDataSourceSubscribedEvent> context)
        {
            var datasource = await _datasourceCommandRepository.Get(context.Message.DataSourceId, CancellationToken.None);
            datasource.IncrementFollower();
            datasource.IncrementTopic(context.Message.Title);
            await _datasourceCommandRepository.Update(new[] { datasource }, CancellationToken.None);
            await _datasourceCommandRepository.SaveChanges(CancellationToken.None);
        }
    }
}
