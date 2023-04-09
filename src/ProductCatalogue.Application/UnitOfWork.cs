using MediatR;
using ProductCatalogue.Domain.BaseTypes;
using ProductCatalogue.Domain.Repositories;

namespace ProductCatalogue.Application;

public class UnitOfWork : IUnitOfWork
{
    private readonly IMediator _mediator;

    public UnitOfWork(IMediator mediator)
    {
        _mediator = mediator;
    }

    public void Commit(IAggregateRoot aggregateRoot)
    {
        foreach (var evnt in aggregateRoot.DeQueueEvents())
        {
            _mediator.Publish(evnt);
        }
    }
}
