
using ProductCatalogue.Domain.BaseTypes;

namespace ProductCatalogue.Domain.Repositories;

public interface IRepository<T> where T : AggregateRoot
{
    public void Save(T aggregate);
}
