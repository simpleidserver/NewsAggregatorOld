using NewsAggregator.Core.Domains.Sessions;
using NewsAggregator.Core.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.ML.EventHandlers
{
    public abstract class BaseSessionEventConsumer
    {
        private readonly ISessionCommandRepository _sessionCommandRepository;

        public BaseSessionEventConsumer(ISessionCommandRepository sessionCommandRepository)
        {
            _sessionCommandRepository = sessionCommandRepository;
        }

        protected async Task Consume(string sessionId, string userId, Action<SessionAggregate> callback)
        {
            var session = await _sessionCommandRepository.Get(sessionId, CancellationToken.None);
            if (session == null)
            {
                session = SessionAggregate.Create(sessionId, userId);
                callback(session);
                await _sessionCommandRepository.Add(session, CancellationToken.None);
            }
            else
            {
                callback(session);
                await _sessionCommandRepository.Update(session, CancellationToken.None);
            }

            await _sessionCommandRepository.SaveChanges(CancellationToken.None);
        }
    }
}
