using Application.Common;
using Application.Responses;
using Domain.Entities;
using MediatR;

namespace Application.Features.Orders.Queries.GetOrdersByCustomerId;

public record GetOrdersByCustomerIdQuery(Guid CustomerId, int PageNumber, int PageSize) : IRequest<PaginatedResult<IList<OrderResponse>>>;