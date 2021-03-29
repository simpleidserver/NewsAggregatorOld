using NewsAggregator.Domain;
using NewsAggregator.Domain.Sessions.Events;
using NewsAggregator.ML.Helpers;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.ML.EventHandlers
{
    public class SessionEventHandler : IEventHandler<SessionInteractionOccuredEvent>
    {
        #region Handle events

        public Task Handle(SessionInteractionOccuredEvent evt, CancellationToken cancellationToken)
        {
            EnrichCSVFile(evt);
            return Task.CompletedTask;
        }

        #endregion

        protected void EnrichCSVFile(SessionInteractionOccuredEvent evt)
        {
            var csvFilePath = DirectoryHelper.GetCSVSessionFilePath(evt.PersonId, evt.SessionId);
            DirectoryHelper.CreateFile(csvFilePath);
            File.AppendAllLines(csvFilePath, new string[] { $"{evt.EventType.Id},{evt.ArticleId},{evt.ArticleLanguage}"});
        }
    }
}
