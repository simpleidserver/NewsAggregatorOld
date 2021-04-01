
namespace NewsAggregator.Core.Domains
{
    public class DomainEvent
    {
        public DomainEvent(string id, string aggregateId, int version)
        {
            Id = id;
            AggregateId = aggregateId;
            Version = version;
        }

        public string Id { get; private set; }
        public string AggregateId { get; private set; }
        public int Version { get; private set; }
    }
}
