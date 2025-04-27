using Application.Common;
using Application.Responses;
using MediatR;

namespace Application.Features.Customers.Commands.DeleteCustomer;

public record DeleteCustomerCommand(Guid Id) : IRequest<Result<CustomerResponse>>;