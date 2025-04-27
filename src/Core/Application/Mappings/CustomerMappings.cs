using Application.Responses;
using Domain.Entities;

namespace Application.Mappings;

internal static class CustomerMappings
{
    public static CustomerResponse ToCustomerResponse(this Customer customer)
    {
        return new(customer.Id, customer.FirstName, customer.LastName, customer.Address, customer.ZipCode);
    }
}