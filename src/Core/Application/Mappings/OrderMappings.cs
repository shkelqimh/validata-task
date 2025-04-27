using Application.Responses;
using Domain.Entities;

namespace Application.Mappings;

internal static class OrderMappings
{
    public static OrderResponse ToOrderResponse(this Order order)
    {
        return new OrderResponse(order.Id, order.Status, order.CreatedOn, order.ModifiedOn, 
            order.OrderItems.Select(orderItem => orderItem.ToOrderItemResponse()).ToList());
    }

    public static OrderItemResponse ToOrderItemResponse(this OrderItem orderItem)
    {
        return new OrderItemResponse(orderItem.Id, orderItem.OrderId, orderItem.ProductId, orderItem.Quantity);
    }
}