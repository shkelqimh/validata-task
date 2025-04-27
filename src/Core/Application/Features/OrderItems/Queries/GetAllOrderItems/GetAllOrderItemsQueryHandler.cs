using Application.Common;
using Application.Mappings;
using Application.Responses;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.OrderItems.Queries.GetAllOrderItems;

public class GetAllOrderItemsQueryHandler : IRequestHandler<GetAllOrderItemsQuery, PaginatedResult<IList<OrderItemResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllOrderItemsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<PaginatedResult<IList<OrderItemResponse>>> Handle(GetAllOrderItemsQuery request, CancellationToken cancellationToken)
    {
        var orderItems = await _unitOfWork.OrderItems.GetAsync(orderItem => orderItem.OrderId == request.OrderId,
            request.PageNumber, request.PageSize);
        
        var count = await _unitOfWork.OrderItems.CountAsync(orderItem => orderItem.OrderId == request.OrderId);
        
        return PaginatedResult<IList<OrderItemResponse>>.Success(orderItems.Select(orderItem => orderItem.ToOrderItemResponse()).ToList(), request.PageNumber, request.PageSize, count);
    }
}