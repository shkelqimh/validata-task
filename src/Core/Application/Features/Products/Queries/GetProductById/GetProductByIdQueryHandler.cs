using Application.Common;
using Application.Mappings;
using Application.Responses;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(request.Id);

        if (product is null)
        {
            return Result<ProductResponse>.Failure(new(){{nameof(GetProductByIdQueryHandler), ["Product not found"]}});
        }

        return Result<ProductResponse>.Success(product.ToProductResponse());
    }
}