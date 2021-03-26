namespace NewsAggregator.Domain
{
    public class DomainEvent
    {
        public DomainEvent(string id)
        {
            Id = id;
        }

        public string Id { get; private set; }
    }
}
