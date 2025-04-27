using Application.Common;
using Application.Responses;
using MediatR;

namespace Application.Features.Orders.Queries.GetOrders;

public record GetOrdersQuery(int PageNumber, int PageSize) : IRequest<PaginatedResult<List<OrderResponse>>>;