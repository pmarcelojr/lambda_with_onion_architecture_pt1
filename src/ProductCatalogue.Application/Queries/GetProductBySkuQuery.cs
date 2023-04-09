using Ardalis.GuardClauses;
using MediatR;
using ProductCatalogue.Application.Dtos;

namespace ProductCatalogue.Application.Queries;

public class GetProductBySkuQuery : IRequest<ProductDto>
{
    public GetProductBySkuQuery()
    {

    }

    public GetProductBySkuQuery(Guid tenantId, string sku)
    {
        TenantId = Guard.Against.Default(tenantId, nameof(tenantId));
        Sku = Guard.Against.NullOrWhiteSpace(sku, nameof(sku));
    }

    public string Sku { get; set; }
    public Guid TenantId { get; set; }
}
