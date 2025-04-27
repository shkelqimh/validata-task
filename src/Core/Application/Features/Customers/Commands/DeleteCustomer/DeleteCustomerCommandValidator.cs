using FluentValidation;

namespace Application.Features.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerCommandValidator()
    {
        RuleFor(customer => customer.Id)
            .NotNull()
            .NotEmpty();
    }
}