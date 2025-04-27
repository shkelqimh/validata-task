using Application.Common;
using Application.Mappings;
using Application.Responses;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.OrderItems.Commands.DeleteOrderItem;

public class DeleteOrderItemCommandHandler : IRequestHandler<DeleteOrderItemCommand, Result<OrderItemResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteOrderItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<OrderItemResponse>> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
    {
        var orderItem = await _unitOfWork.OrderItems.GetByIdAsync(request.Id);

        if (orderItem is null)
        {
            return Result<OrderItemResponse>.Failure(new(){{nameof(DeleteOrderItemCommandHandler), ["Order item not found"]}});
        }

        await _unitOfWork.OrderItems.DeleteAsync(orderItem);

        var hasAffected = await _unitOfWork.SaveChangesAsync() > 0;

        if (!hasAffected)
        {
            return Result<OrderItemResponse>.Failure(new(){{nameof(DeleteOrderItemCommandHandler), ["Failed to delete order item"]}});
        }

        return Result<OrderItemResponse>.Success(orderItem.ToOrderItemResponse());
    }
}