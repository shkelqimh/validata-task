using Application.Common;
using Application.Responses;
using MediatR;

namespace Application.Features.Customers.Commands.CreateCustomer;

public record CreateCustomerCommand(string FirstName, string LastName, string Address, string ZipCode) : IRequest<Result<CustomerResponse>>;