using Application.Features.Orders.Commands.DeleteOrder;
using Application.Features.Orders.Queries.GetOrderById;
using Application.Features.Orders.Queries.GetOrders;
using Application.Features.Orders.Queries.GetOrdersByCustomerId;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Mappings;
using WebApi.Requests;

namespace WebApi.Controllers;

[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(PaginationRequest request)
    {
        return Ok(await _mediator.Send(new GetOrdersQuery(request.PageNumber, request.PageSize)));
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetOrderByIdQuery(id));

        if (result.HasError)
        {
            return NotFound(result.Errors);
        }

        return Ok(result.Data);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
    {
        var result = await _mediator.Send(request.ToCreateOrderCommand());

        if (result.HasError)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result.Data);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateOrderRequest request)
    {
        var result = await _mediator.Send(request.ToUpdateOrderCommand(id));

        if (result.HasError)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result.Data);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new DeleteOrderCommand(id));

        if (result.HasError)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }
}