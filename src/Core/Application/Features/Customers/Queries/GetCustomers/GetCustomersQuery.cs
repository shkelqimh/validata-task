using Application.Common;
using Application.Responses;
using MediatR;

namespace Application.Features.Customers.Queries.GetCustomers;

public record GetCustomersQuery(int PageNumber, int PageSize) : IRequest<PaginatedResult<List<CustomerResponse>>>;