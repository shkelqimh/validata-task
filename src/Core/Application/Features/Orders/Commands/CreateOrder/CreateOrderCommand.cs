using Application.Common;
using Application.Features.OrderItems.Commands.CreateOrderItem;
using Application.Responses;
using MediatR;

namespace Application.Features.Orders.Commands.CreateOrder;

public record CreateOrderCommand(Guid CustomerId, List<CreateOrderItemCommand> OrderItems) : IRequest<Result<OrderResponse>>;