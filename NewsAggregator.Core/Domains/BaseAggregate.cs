using System.Collections.Generic;

namespace NewsAggregator.Core.Domains
{
    public abstract class BaseAggregate
    {
        public BaseAggregate()
        {
            DomainEvts = new List<DomainEvent>();
        }
        
        public string Id { get; set; }
        public int Version { get; set; }
        public ICollection<DomainEvent> DomainEvts { get; private set; }

        protected abstract void Handle(dynamic evt);
    }
}