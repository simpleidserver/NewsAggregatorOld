using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Core.Domains
{
    public interface IEventHandler<T> where T : DomainEvent
    {
        Task Handle(T evt, CancellationToken cancellationToken);
    }
}
