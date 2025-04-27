using Application.Common;
using Application.Mappings;
using Application.Responses;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Result<CustomerResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCustomerCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<CustomerResponse>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(request.Id);

        if (customer is null)
        {
            return Result<CustomerResponse>.Failure(new(){{nameof(UpdateCustomerCommandHandler), ["Customer not found"]}});
        }
        
        customer.Update(request.FirstName, request.LastName, request.Address, request.ZipCode);
        
        await _unitOfWork.Customers.UpdateAsync(customer);
        
        var hasAffected = await _unitOfWork.SaveChangesAsync() > 0;

        if (!hasAffected)
        {
            return Result<CustomerResponse>.Failure(new(){{nameof(UpdateCustomerCommandHandler), ["Failed to update customer"]}});
        }

        return Result<CustomerResponse>.Success(customer.ToCustomerResponse());
    }
}