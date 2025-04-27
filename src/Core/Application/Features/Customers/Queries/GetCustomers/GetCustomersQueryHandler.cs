using Application.Common;
using Application.Mappings;
using Application.Responses;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Customers.Queries.GetCustomers;

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, PaginatedResult<List<CustomerResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCustomersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<PaginatedResult<List<CustomerResponse>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await _unitOfWork.Customers.GetAllAsync(request.PageNumber, request.PageSize);

        var count = await _unitOfWork.Customers.CountAsync();

        return PaginatedResult<List<CustomerResponse>>.Success(customers.Select(customer => customer.ToCustomerResponse())
            .ToList(), request.PageNumber, request.PageSize, count);
    }
}