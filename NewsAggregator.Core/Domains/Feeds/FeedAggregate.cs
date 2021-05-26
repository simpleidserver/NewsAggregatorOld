using NewsAggregator.Core.Domains.Feeds.Events;
using NewsAggregator.Core.Exceptions;
using NewsAggregator.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsAggregator.Core.Domains.Feeds
{
    public class FeedAggregate : BaseAggregate
    {
        private FeedAggregate()
        {
            DataSources = new List<FeedDatasource>();
        }

        public string UserId { get; set; }
        public string Title { get; set; }
        public ICollection<FeedDatasource> DataSources { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }

        #region Actions

        public FeedDataSourceSubscribedEvent SubscribeDataSource(string userId, string datasourceId)
        {
            var evt = new FeedDataSourceSubscribedEvent(Guid.NewGuid().ToString(), Id, Version + 1, Title, userId, datasourceId, DateTime.UtcNow);
            Handle(evt);
            DomainEvts.Add(evt);
            return evt;
        }

        public void UnsubscribeDataSource(string userId, string datasourceId)
        {
            var evt = new FeedDataSourceUnsubscribedEvent(Guid.NewGuid().ToString(), Id, Version + 1, userId, datasourceId, DateTime.UtcNow);
            Handle(evt);
            DomainEvts.Add(evt);
        }

        public void Remove(string userId)
        {
            var evt = new FeedRemovedEvent(Guid.NewGuid().ToString(), Id, Version + 1, userId);
            Handle(evt);
            DomainEvts.Add(evt);
        }

        #endregion

        public static FeedAggregate Create(string userId, string title)
        {
            var result = new FeedAggregate();
            var evt = new FeedAddedEvent(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), 0, userId, title, DateTime.UtcNow);
            result.Handle(evt);
            return result;
        }

        protected override void Handle(dynamic evt)
        {
            Handle(evt);
        }

        private void Handle(FeedAddedEvent evt)
        {
            if (string.IsNullOrWhiteSpace(evt.Title))
            {
                throw new DomainException(Global.TitleIsEmpty);
            }

            if (string.IsNullOrWhiteSpace(evt.UserId))
            {
                throw new DomainException(Global.UserIdIsEmpty);
            }

            Id = evt.AggregateId;
            UserId = evt.UserId;
            Title = evt.Title;
            CreateDateTime = evt.CreateDateTime;
        }

        private void Handle(FeedDataSourceSubscribedEvent evt)
        {
            if (UserId != evt.UserId)
            {
                throw new DomainException(Global.UserNotAuthorizedToSubscribe);
            }

            if (DataSources.Any(d => d.DatasourceId == evt.DataSourceId))
            {
                throw new DomainException(string.Format(Global.DatasourceAlreadyExists, evt.DataSourceId));
            }

            DataSources.Add(FeedDatasource.Create(evt.DataSourceId));
            UpdateDateTime = evt.CreateDateTime;
            Version = evt.Version;
        }

        private void Handle(FeedDataSourceUnsubscribedEvent evt)
        {
            if (UserId != evt.UserId)
            {
                throw new DomainException(Global.UserNotAuthorizedToUnSubscribe);
            }

            var datasource = DataSources.FirstOrDefault(d => d.DatasourceId == evt.DataSourceId);
            if (datasource == null)
            {
                throw new DomainException(string.Format(Global.DataSourceDoesntExist, evt.DataSourceId));
            }

            DataSources.Remove(datasource);
            UpdateDateTime = evt.DeletionDateTime;
            Version = evt.Version;
        }

        private void Handle(FeedRemovedEvent evt)
        {
            if (UserId != evt.UserId)
            {
                throw new DomainException(string.Format(Global.CannotRemoveFeed, evt.UserId));
            }
        }
    }
}