using Application.Common;
using Application.Mappings;
using Application.Responses;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.OrderItems.Commands.CreateOrderItem;

public class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand, Result<OrderItemResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<OrderItemResponse>> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
    {
        var orderItem = OrderItem.Create(request.OrderId!.Value, request.ProductId, request.Quantity);
        
        await _unitOfWork.OrderItems.AddAsync(orderItem);
        
        var hasAffected = await _unitOfWork.SaveChangesAsync() > 0;

        if (!hasAffected)
        {
            return Result<OrderItemResponse>.Failure(new(){{nameof(CreateOrderItemCommandHandler), ["Failed to add order item"]}});
        }

        return Result<OrderItemResponse>.Success(orderItem.ToOrderItemResponse());
    }
}