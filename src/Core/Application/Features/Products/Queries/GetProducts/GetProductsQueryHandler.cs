using Application.Common;
using Application.Mappings;
using Application.Responses;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Products.Queries.GetProducts;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PaginatedResult<List<ProductResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedResult<List<ProductResponse>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Products.GetAllAsync(request.PageNumber, request.PageSize);

        var mappedProducts = products
            .Select(p => p.ToProductResponse())
            .ToList();
        
        var count = await _unitOfWork.Products.CountAsync();

        return PaginatedResult<List<ProductResponse>>.Success(mappedProducts, request.PageNumber, request.PageSize, count);
    }
}