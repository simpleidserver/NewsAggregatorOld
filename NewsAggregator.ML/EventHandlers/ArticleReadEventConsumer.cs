using MassTransit;
using NewsAggregator.Core.Domains.Articles.Events;
using NewsAggregator.Core.Repositories;
using System.Threading.Tasks;

namespace NewsAggregator.ML.EventHandlers
{
    public class ArticleReadEventConsumer : BaseSessionEventConsumer, IConsumer<ArticleReadEvent>
    {
        public ArticleReadEventConsumer(ISessionCommandRepository sessionCommandRepository) : base(sessionCommandRepository) { }

        public Task Consume(ConsumeContext<ArticleReadEvent> context)
        {
            return base.Consume(context.Message.SessionId, context.Message.UserId, (session) =>
            {
                session.Read(context.Message.AggregateId, context.Message.Language, context.Message.ActionDateTime);
            });
        }
    }
}