using Domain.Constants;
using FluentValidation;

namespace Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(product => product.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(Constants.Product.NameMinLength)
            .MaximumLength(Constants.Product.NameMaxLength);

        RuleFor(product => product.Price)
            .NotNull()
            .GreaterThan(0);
    }
}