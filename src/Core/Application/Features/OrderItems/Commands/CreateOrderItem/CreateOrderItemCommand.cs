using Application.Common;
using Application.Responses;
using MediatR;

namespace Application.Features.OrderItems.Commands.CreateOrderItem;

public record CreateOrderItemCommand(Guid? OrderId, Guid ProductId, int Quantity) : IRequest<Result<OrderItemResponse>>;