using Application.Common;
using Application.Responses;
using MediatR;

namespace Application.Features.OrderItems.Queries.GetAllOrderItems;

public record GetAllOrderItemsQuery(Guid OrderId, int PageNumber, int PageSize) : IRequest<PaginatedResult<IList<OrderItemResponse>>>;