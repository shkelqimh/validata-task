using Application.Common;
using Application.Responses;
using MediatR;

namespace Application.Features.Orders.Queries.GetOrderById;

public record GetOrderByIdQuery(Guid Id) : IRequest<Result<OrderResponse>>;