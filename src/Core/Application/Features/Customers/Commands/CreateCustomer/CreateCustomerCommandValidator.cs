using Domain.Constants;
using FluentValidation;

namespace Application.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(customer => customer.FirstName)
            .NotEmpty()
            .MaximumLength(Constants.Customer.FirstNameMaxLength);

        RuleFor(customer => customer.LastName)
            .NotEmpty()
            .MaximumLength(Constants.Customer.LastNameMaxLength);
        
        
        RuleFor(customer => customer.Address)
            .NotEmpty()
            .MaximumLength(Constants.Customer.AddressMaxLength);
        
        
        RuleFor(customer => customer.ZipCode)
            .NotEmpty()
            .MaximumLength(Constants.Customer.ZipCodeMaxLength);
    }
}