using Application.Common;
using Application.Mappings;
using Application.Responses;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.OrderItems.Commands.UpdateOrderItem;

public class UpdateOrderItemCommandHandler : IRequestHandler<UpdateOrderItemCommand, Result<OrderItemResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOrderItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<OrderItemResponse>> Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
    {
        var orderItem = await _unitOfWork.OrderItems.GetByIdAsync(request.Id);

        if (orderItem is null)
        {
            return Result<OrderItemResponse>.Failure(new(){{nameof(UpdateOrderItemCommandHandler), ["Order item not found"]}});
        }
        
        orderItem.Update(orderItem.ProductId, orderItem.Quantity);
        
        await _unitOfWork.OrderItems.UpdateAsync(orderItem);

        var hasAffected = await _unitOfWork.SaveChangesAsync() > 0;
        
        if (!hasAffected)
        {
            return Result<OrderItemResponse>.Failure(new(){{nameof(UpdateOrderItemCommandHandler), ["Failed to update order item"]}});
        }

        return Result<OrderItemResponse>.Success(orderItem.ToOrderItemResponse());
    }
}