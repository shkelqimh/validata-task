using Domain.Interfaces;
using FluentValidation;

namespace Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(order => order.CustomerId)
            .NotNull()
            .NotEmpty();

        RuleFor(order => order.OrderItems)
            .NotEmpty();
    }
}