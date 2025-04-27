using Application.Common;
using Application.Mappings;
using Application.Responses;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Result<OrderResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteOrderCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<OrderResponse>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(request.Id);

        if (order is null)
        {
            return Result<OrderResponse>.Failure(new(){{nameof(DeleteOrderCommandHandler), ["Order not found"]}});
        }
        
        await _unitOfWork.Orders.DeleteAsync(order);
        
        var hasAffected = await _unitOfWork.SaveChangesAsync() > 0;

        if (!hasAffected)
        {
            return Result<OrderResponse>.Failure(new(){{nameof(DeleteOrderCommandHandler), ["Failed to delete order"]}});
        }

        return Result<OrderResponse>.Success(order.ToOrderResponse());
    }
}