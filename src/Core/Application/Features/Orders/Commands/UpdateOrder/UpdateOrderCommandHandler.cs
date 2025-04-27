using Application.Common;
using Application.Mappings;
using Application.Responses;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Result<OrderResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOrderCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<OrderResponse>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(request.Id, order => order.OrderItems);
        
        if (order is null)
        {
            return Result<OrderResponse>.Failure(new(){{nameof(UpdateOrderCommandHandler), [$"Order with id {request.Id} was not found"]}});
        }
        
        foreach (var orderItemRequest in request.OrderItems)
        {
            var existingItem = order.OrderItems.FirstOrDefault(x => x.Id == orderItemRequest.Id);

            if (existingItem is not null)
            {
                existingItem.Update(orderItemRequest.ProductId, orderItemRequest.Quantity);
            }
            else
            {
                var newItem = OrderItem.CreateWithoutId(order.Id, orderItemRequest.ProductId, orderItemRequest.Quantity);
                
                order.AddOrderItem(newItem);
            }
        }
        
        var incomingIds = request.OrderItems.Select(x => x.Id).ToHashSet();

        order.RemoveOrderItems(x => !incomingIds.Contains(x.Id));

        await _unitOfWork.Orders.UpdateAsync(order);
        
        var hasAffected = await _unitOfWork.SaveChangesAsync() > 0;

        if (!hasAffected)
        {
            return Result<OrderResponse>.Failure(new(){{nameof(UpdateOrderCommandHandler), ["Failed to update order"]}});
        }
        
        return Result<OrderResponse>.Success(order.ToOrderResponse());
    }
}