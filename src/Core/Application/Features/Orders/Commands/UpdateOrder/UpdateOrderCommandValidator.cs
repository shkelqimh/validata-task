using FluentValidation;

namespace Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(order => order.Id)
            .NotEmpty()
            .NotNull();
    }
}