using MassTransit;
using NewsAggregator.Core.Domains.Articles.Events;
using NewsAggregator.Core.Repositories;
using System.Threading.Tasks;

namespace NewsAggregator.ML.EventHandlers
{
    public class ArticleLikedEventConsumer : BaseSessionEventConsumer, IConsumer<ArticleLikedEvent>
    {
        public ArticleLikedEventConsumer(ISessionCommandRepository sessionCommandRepository) : base(sessionCommandRepository) { }

        public Task Consume(ConsumeContext<ArticleLikedEvent> context)
        {
            return base.Consume(context.Message.SessionId, context.Message.UserId, (session) =>
            {
                session.Like(context.Message.AggregateId, context.Message.Language, context.Message.ActionDateTime);
            });
        }
    }
}
