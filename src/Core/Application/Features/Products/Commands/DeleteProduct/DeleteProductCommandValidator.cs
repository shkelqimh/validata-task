using FluentValidation;

namespace Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(product => product.Id)
            .NotNull()
            .NotEmpty();
    }
}