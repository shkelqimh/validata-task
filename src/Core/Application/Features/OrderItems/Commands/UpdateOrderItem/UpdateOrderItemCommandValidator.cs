using Domain.Interfaces;
using FluentValidation;

namespace Application.Features.OrderItems.Commands.UpdateOrderItem;

public class UpdateOrderItemCommandValidator : AbstractValidator<UpdateOrderItemCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOrderItemCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        RuleFor(orderItem => orderItem.Id)
            .NotNull()
            .NotEmpty();

        RuleFor(orderItem => orderItem.ProductId)
            .NotNull()
            .NotEmpty()
            .MustAsync((productId, _) => Exist(productId));
    }

    private async Task<bool> Exist(Guid productId)
        => await _unitOfWork.Products.ExistsAsync(productId);
}