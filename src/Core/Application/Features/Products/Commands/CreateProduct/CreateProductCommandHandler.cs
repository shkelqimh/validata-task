using Application.Common;
using Application.Mappings;
using Application.Responses;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<ProductResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IUnitOfWork uow)
    {
        _unitOfWork = uow;
    }

    public async Task<Result<ProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Product.Create(request.Name, request.Price);
        
        await _unitOfWork.Products.AddAsync(product);
        
        var hasAffected = await _unitOfWork.SaveChangesAsync() > 0;

        if (!hasAffected)
        {
            return Result<ProductResponse>.Failure(new(){{nameof(CreateProductCommandHandler), ["Failed to create product"]}});
        }
        
        return Result<ProductResponse>.Success(product.ToProductResponse());
    }
}