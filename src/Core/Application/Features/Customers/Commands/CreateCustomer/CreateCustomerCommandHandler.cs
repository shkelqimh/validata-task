using Application.Common;
using Application.Mappings;
using Application.Responses;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result<CustomerResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateCustomerCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CustomerResponse>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = Customer.Create(request.FirstName, request.LastName, request.Address, request.ZipCode);

        await _unitOfWork.Customers.AddAsync(customer);
        
        var hasAffected = await _unitOfWork.SaveChangesAsync() > 0;

        if (!hasAffected)
        {
            return Result<CustomerResponse>.Failure(new (){{ nameof(CreateCustomerCommandHandler), ["Failed to create customer"] }});
        }
        
        return Result<CustomerResponse>.Success(customer.ToCustomerResponse());
    }
}