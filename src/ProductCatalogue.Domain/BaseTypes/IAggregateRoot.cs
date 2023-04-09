

namespace ProductCatalogue.Domain.BaseTypes;

public interface IAggregateRoot : IEntity
{
    public void QueueEvent(IDomainEvent evnt);
    public IEnumerable<IDomainEvent> DeQueueEvents();
}
