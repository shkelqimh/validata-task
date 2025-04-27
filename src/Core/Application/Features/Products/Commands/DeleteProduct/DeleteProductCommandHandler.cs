using Application.Common;
using Application.Mappings;
using Application.Responses;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<ProductResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<ProductResponse>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(request.Id);
        
        if (product is null)
        {
            return Result<ProductResponse>.Failure(new (){{nameof(DeleteProductCommandHandler), ["Product not found"]}});
        }
        
        await _unitOfWork.Products.DeleteAsync(product);

        var hasAffected = await _unitOfWork.SaveChangesAsync() > 0;

        if (!hasAffected)
        {
            return Result<ProductResponse>.Failure(new(){{nameof(DeleteProductCommandHandler), ["Failed to delete product"]}});
        }
        
        return Result<ProductResponse>.Success(product.ToProductResponse());
    }
}