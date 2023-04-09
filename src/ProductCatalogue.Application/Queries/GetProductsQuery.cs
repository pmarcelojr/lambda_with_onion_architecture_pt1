using Ardalis.GuardClauses;
using MediatR;
using ProductCatalogue.Application.Dtos;

namespace ProductCatalogue.Application.Queries;

public class GetProductsQuery : IRequest<IEnumerable<ProductDto>>
{
    public Guid TenantId { get; private set; }

    public GetProductsQuery(Guid tenantId)
    {
        TenantId = Guard.Against.Default(tenantId, nameof(tenantId));
    }
}
