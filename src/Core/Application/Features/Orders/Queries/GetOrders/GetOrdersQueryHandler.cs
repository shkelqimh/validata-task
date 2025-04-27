using Application.Common;
using Application.Mappings;
using Application.Responses;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Orders.Queries.GetOrders;

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, PaginatedResult<List<OrderResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOrdersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedResult<List<OrderResponse>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _unitOfWork.Orders.GetAllAsync(request.PageNumber, request.PageSize, order => order.OrderItems);

        var mappedOrders = orders.Select(order => order.ToOrderResponse()).ToList();
        
        var count = await _unitOfWork.Orders.CountAsync();
        
        return PaginatedResult<List<OrderResponse>>.Success(mappedOrders, request.PageNumber, request.PageSize, count);
    }
}