using Application.Features.OrderItems.Commands.CreateOrderItem;
using Application.Features.OrderItems.Commands.UpdateOrderItem;
using Application.Features.Orders.Commands.CreateOrder;
using Application.Features.Orders.Commands.UpdateOrder;
using WebApi.Requests;

namespace WebApi.Mappings;

public static class OrderMappings
{
    public static CreateOrderCommand ToCreateOrderCommand(this CreateOrderRequest order)
    {
        return new CreateOrderCommand(order.CustomerId,
            order.OrderItems.Select(orderItem => orderItem.ToCreateOrderItemCommand(null)).ToList());
    }

    public static CreateOrderItemCommand ToCreateOrderItemCommand(this CreateOrderItemRequest orderItem, Guid? orderId)
    {
        return new CreateOrderItemCommand(orderId, orderItem.ProductId, orderItem.Quantity);
    }

    public static UpdateOrderCommand ToUpdateOrderCommand(this UpdateOrderRequest order, Guid orderId)
    {
        return new UpdateOrderCommand(orderId, order.CustomerId, order.Status,
            order.OrderItems.Select(orderItem => orderItem.ToUpdateOrderItemCommand()).ToList());
    }

    public static UpdateOrderItemCommand ToUpdateOrderItemCommand(this UpdateOrderItemRequest orderItem)
    {
        return new UpdateOrderItemCommand(orderItem.Id, orderItem.ProductId, orderItem.Quantity);
    }
}