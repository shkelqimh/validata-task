using Application.Common;
using Application.Features.OrderItems.Commands.UpdateOrderItem;
using Application.Responses;
using MediatR;

namespace Application.Features.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(Guid Id, Guid CustomerId, string Status, List<UpdateOrderItemCommand> OrderItems) : IRequest<Result<OrderResponse>>;