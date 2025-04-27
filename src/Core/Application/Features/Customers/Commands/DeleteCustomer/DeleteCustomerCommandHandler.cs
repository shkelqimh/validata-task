using Application.Common;
using Application.Mappings;
using Application.Responses;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Result<CustomerResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCustomerCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<CustomerResponse>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(request.Id);

        if (customer is null)
        {
            return Result<CustomerResponse>.Failure(new(){{nameof(DeleteCustomerCommandHandler), ["Customer not found"]}});
        }

        await _unitOfWork.Customers.DeleteAsync(customer);

        var hasAffected = await _unitOfWork.SaveChangesAsync() > 0;

        if (!hasAffected)
        {
            return Result<CustomerResponse>.Failure(new(){{nameof(DeleteCustomerCommandHandler), ["Failed to delete customer"]}});
        }

        return Result<CustomerResponse>.Success(customer.ToCustomerResponse());
    }
}