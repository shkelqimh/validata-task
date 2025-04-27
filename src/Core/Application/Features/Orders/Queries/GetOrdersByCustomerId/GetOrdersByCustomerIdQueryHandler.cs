using System.Linq.Expressions;
using Application.Common;
using Application.Mappings;
using Application.Responses;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Orders.Queries.GetOrdersByCustomerId;

public class GetOrdersByCustomerIdQueryHandler : IRequestHandler<GetOrdersByCustomerIdQuery, PaginatedResult<IList<OrderResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOrdersByCustomerIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<PaginatedResult<IList<OrderResponse>>> Handle(GetOrdersByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Order,bool>> predicate = order => order.CustomerId == request.CustomerId;
        
        var orders = await _unitOfWork.Orders.GetAsync(predicate, request.PageNumber, request.PageSize,
            order => order.OrderItems);
        
        var mappedResponses = orders.Select(order => order.ToOrderResponse()).ToList();

        var count = await _unitOfWork.Orders.CountAsync(predicate);

        return PaginatedResult<IList<OrderResponse>>.Success(mappedResponses, request.PageNumber, request.PageSize, count);

    }
}