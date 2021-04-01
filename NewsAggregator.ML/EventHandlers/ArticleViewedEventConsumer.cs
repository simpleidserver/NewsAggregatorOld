using MassTransit;
using NewsAggregator.Core.Domains.Articles.Events;
using NewsAggregator.Core.Repositories;
using System.Threading.Tasks;

namespace NewsAggregator.ML.EventHandlers
{
    public class ArticleViewedEventConsumer : BaseSessionEventConsumer, IConsumer<ArticleViewedEvent>
    {
        public ArticleViewedEventConsumer(ISessionCommandRepository sessionCommandRepository) : base(sessionCommandRepository) { }

        public Task Consume(ConsumeContext<ArticleViewedEvent> context)
        {
            return base.Consume(context.Message.SessionId, context.Message.UserId, (session) =>
            {
                session.View(context.Message.AggregateId, context.Message.Language, context.Message.ActionDateTime);
            });
        }
    }
}