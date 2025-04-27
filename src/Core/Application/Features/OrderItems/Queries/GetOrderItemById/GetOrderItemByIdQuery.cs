using Application.Common;
using Application.Responses;
using MediatR;

namespace Application.Features.OrderItems.Queries.GetOrderItemById;

public record GetOrderItemByIdQuery(Guid OrderId, Guid OrderItemId) : IRequest<Result<OrderItemResponse>>;