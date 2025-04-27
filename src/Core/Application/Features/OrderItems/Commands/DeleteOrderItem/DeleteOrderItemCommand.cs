using Application.Common;
using Application.Responses;
using MediatR;

namespace Application.Features.OrderItems.Commands.DeleteOrderItem;

public record DeleteOrderItemCommand(Guid Id) : IRequest<Result<OrderItemResponse>>;