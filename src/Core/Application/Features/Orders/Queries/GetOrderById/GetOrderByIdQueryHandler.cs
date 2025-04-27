using Application.Common;
using Application.Mappings;
using Application.Responses;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Orders.Queries.GetOrderById;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Result<OrderResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOrderByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<OrderResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(request.Id);

        if (order is null)
        {
            return Result<OrderResponse>.Failure(new(){{nameof(GetOrderByIdQueryHandler), ["Order not found"]}});
        }

        return Result<OrderResponse>.Success(order.ToOrderResponse());
    }
}