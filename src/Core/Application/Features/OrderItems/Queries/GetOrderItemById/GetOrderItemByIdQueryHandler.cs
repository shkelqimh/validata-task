using Application.Common;
using Application.Mappings;
using Application.Responses;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.OrderItems.Queries.GetOrderItemById;

public class GetOrderItemByIdQueryHandler : IRequestHandler<GetOrderItemByIdQuery, Result<OrderItemResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOrderItemByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<OrderItemResponse>> Handle(GetOrderItemByIdQuery request, CancellationToken cancellationToken)
    {
        var orderItem = await _unitOfWork.OrderItems.GetByIdAsync(request.OrderItemId);

        if (orderItem is null)
        {
            return Result<OrderItemResponse>.Failure(new(){{nameof(GetOrderItemByIdQueryHandler), ["Order item not found"]}});
        }

        return Result<OrderItemResponse>.Success(orderItem.ToOrderItemResponse());
    }
}