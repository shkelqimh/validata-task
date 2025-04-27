using FluentValidation;

namespace Application.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(order => order.Id)
            .NotNull()
            .NotEmpty();
    }
}