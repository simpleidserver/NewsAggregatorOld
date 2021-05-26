using MassTransit;
using NewsAggregator.Core.Domains.Articles.Events;
using NewsAggregator.Core.Repositories;
using System.Threading.Tasks;

namespace NewsAggregator.ML.EventHandlers
{
    public class ArticleUnreadEventConsumer : BaseSessionEventConsumer, IConsumer<ArticleUnreadEvent>
    {
        public ArticleUnreadEventConsumer(ISessionCommandRepository sessionCommandRepository) : base(sessionCommandRepository) { }

        public Task Consume(ConsumeContext<ArticleUnreadEvent> context)
        {
            return base.Consume(context.Message.SessionId, context.Message.UserId, (session) =>
            {
                session.Unread(context.Message.AggregateId, context.Message.Language, context.Message.ActionDateTime);
            });
        }
    }
}