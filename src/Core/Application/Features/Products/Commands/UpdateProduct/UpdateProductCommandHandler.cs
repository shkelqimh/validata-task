using Application.Common;
using Application.Mappings;
using Application.Responses;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<ProductResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ProductResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(request.Id);

        if (product is null)
        {
            return Result<ProductResponse>.Failure(new(){{nameof(UpdateProductCommandHandler), ["Product not found"]}});
        }

        product.Update(request.Name, request.Price);

        await _unitOfWork.Products.UpdateAsync(product);

        var hasAffected = await _unitOfWork.SaveChangesAsync() > 0;

        if (hasAffected)
        {
            return Result<ProductResponse>.Success(product.ToProductResponse());
        }

        return Result<ProductResponse>.Failure(new(){{nameof(UpdateProductCommandHandler), ["Failed to update product"]}});
    }
}