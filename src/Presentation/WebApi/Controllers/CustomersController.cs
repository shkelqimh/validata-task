using Application.Features.Customers.Commands.CreateCustomer;
using Application.Features.Customers.Commands.DeleteCustomer;
using Application.Features.Customers.Commands.UpdateCustomer;
using Application.Features.Customers.Queries.GetCustomerById;
using Application.Features.Customers.Queries.GetCustomers;
using Application.Features.Orders.Queries.GetOrdersByCustomerId;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Requests;

namespace WebApi.Controllers;

[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get(PaginationRequest request)
    {
        return Ok(await _mediator.Send(new GetCustomersQuery(request.PageNumber, request.PageSize)));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetCustomerByIdQuery(id));

        if (result.HasError)
        {
            return NotFound(result.Errors);
        }

        return Ok(result.Data);
    }
    
    [HttpGet("{id:guid}/orders")]
    public async Task<IActionResult> GetOrders(Guid id, PaginationRequest request)
    {
        return Ok(await _mediator.Send(new GetOrdersByCustomerIdQuery(id, request.PageNumber, request.PageSize)));
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerRequest request)
    {
        var result = await _mediator.Send(new CreateCustomerCommand(request.FirstName, request.LastName,
            request.Address, request.ZipCode));

        if (result.HasError)
        {
            return BadRequest(result.Errors);
        }

        return Created("", result.Data);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCustomerRequest request)
    {
        var result = await _mediator.Send(new UpdateCustomerCommand(id, request.FirstName, request.LastName,
            request.Address, request.ZipCode));

        if (result.HasError)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result.Data);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new DeleteCustomerCommand(id));

        if (result.HasError)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }
}