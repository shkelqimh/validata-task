using Application.Common;
using Application.Responses;
using MediatR;

namespace Application.Features.OrderItems.Commands.UpdateOrderItem;

public record UpdateOrderItemCommand(Guid Id, Guid ProductId, int Quantity) : IRequest<Result<OrderItemResponse>>;