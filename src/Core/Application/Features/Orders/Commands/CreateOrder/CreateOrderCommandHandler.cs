using Application.Common;
using Application.Mappings;
using Application.Responses;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<OrderResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<OrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        if (!await CustomerExists(request.CustomerId))
        {
            return Result<OrderResponse>.Failure(new(){{nameof(CreateOrderCommandHandler), [$"Customer with id {request.CustomerId} not found"]}});
        }
        
        var order = Order.Create(request.CustomerId);

        foreach (var item in request.OrderItems)
        {
            if (! await ProductExists(item.ProductId))
            {
                return Result<OrderResponse>.Failure(new(){{nameof(CreateOrderCommandHandler), [$"Product with id {item.ProductId} not found"]}});
            }

            var orderItem = OrderItem.Create(order.Id, item.ProductId, item.Quantity);
            order.AddOrderItem(orderItem);
        }

        await _unitOfWork.Orders.AddAsync(order);

        var hasAffected = await _unitOfWork.SaveChangesAsync() > 0;

        if (!hasAffected)
        {
            return Result<OrderResponse>.Failure(new(){{nameof(CreateOrderCommandHandler), ["Failed to add order"]}});
        }
        
        return Result<OrderResponse>.Success(order.ToOrderResponse());
    }

    private async Task<bool> CustomerExists(Guid customerId)
    {
        return await _unitOfWork.Customers.GetByIdAsync(customerId) is not null;
    }

    private async Task<bool> ProductExists(Guid productId)
    {
        return await _unitOfWork.Products.GetByIdAsync(productId) is not null;
    }
}