using Application.Common;
using Application.Responses;
using MediatR;

namespace Application.Features.Customers.Queries.GetCustomerById;

public record GetCustomerByIdQuery(Guid Id) : IRequest<Result<CustomerResponse>>;