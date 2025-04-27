using Application.Common;
using Application.Mappings;
using Application.Responses;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Customers.Queries.GetCustomerById;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Result<CustomerResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCustomerByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<CustomerResponse>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(request.Id);

        if (customer is null)
        {
            return Result<CustomerResponse>.Failure(new(){{nameof(GetCustomerByIdQueryHandler), ["Customer not found"]}});
        }

        return Result<CustomerResponse>.Success(customer.ToCustomerResponse());
    }
}