using FluentValidation;

namespace Application.Features.OrderItems.Commands.CreateOrderItem;

public class CreateOrderItemCommandValidator : AbstractValidator<CreateOrderItemCommand>
{
    public CreateOrderItemCommandValidator()
    {
        RuleFor(orderItem => orderItem.ProductId)
            .NotNull()
            .NotEmpty();

        RuleFor(orderItem => orderItem.Quantity)
            .NotNull()
            .GreaterThan(0);
    }
}