using Application.Common;
using Application.Responses;
using MediatR;

namespace Application.Features.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand(Guid Id) : IRequest<Result<OrderResponse>>;