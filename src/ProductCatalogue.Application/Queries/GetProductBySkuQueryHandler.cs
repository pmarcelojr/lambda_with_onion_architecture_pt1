using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using ProductCatalogue.Application.Dtos;
using ProductCatalogue.Domain.Repositories;

namespace ProductCatalogue.Application.Queries;

public class GetProductBySkuQueryHandler : IRequestHandler<GetProductBySkuQuery, ProductDto>
{
    private readonly IProductsRepository _productsRepository;
    private readonly IMapper _mapper;

    public GetProductBySkuQueryHandler(IProductsRepository productsRepository, IMapper mapper)
    {
        _productsRepository = Guard.Against.Null(productsRepository, nameof(productsRepository));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
    }

    public async Task<ProductDto> Handle(GetProductBySkuQuery request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        var product = await _productsRepository.GetBySkuAsync(request.TenantId, request.Sku, cancellationToken);
        return _mapper.Map<ProductDto>(product);
    }
}

