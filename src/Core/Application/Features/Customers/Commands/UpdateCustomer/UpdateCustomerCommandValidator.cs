using Domain.Constants;
using FluentValidation;

namespace Application.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(customer => customer.Id)
            .NotNull()
            .NotEmpty();
        
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