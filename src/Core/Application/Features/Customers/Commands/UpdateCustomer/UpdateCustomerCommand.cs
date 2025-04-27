using Application.Common;
using Application.Responses;
using MediatR;

namespace Application.Features.Customers.Commands.UpdateCustomer;

public record UpdateCustomerCommand(Guid Id, string FirstName, string LastName, string Address, string ZipCode) : IRequest<Result<CustomerResponse>>;