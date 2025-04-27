using Application.Features.OrderItems.Commands.CreateOrderItem;
using Application.Features.OrderItems.Commands.UpdateOrderItem;
using Application.Features.OrderItems.Queries.GetAllOrderItems;
using Application.Features.OrderItems.Queries.GetOrderItemById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Requests;

namespace WebApi.Controllers;

[Route("api/orders/{order-id:guid}/order-items")]
public class OrderItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromRoute(Name = "order-id")] Guid orderId, PaginationRequest request)
    {
        var result = await _mediator.Send(new GetAllOrderItemsQuery(orderId, request.PageNumber, request.PageSize));
        
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute(Name = "order-id")] Guid orderId, [FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetOrderItemByIdQuery(orderId, id));

        if (result.HasError)
        {
            return NotFound(result.Errors);
        }

        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromRoute(Name = "order-id")] Guid orderId, [FromBody] CreateOrderItemRequest request)
    {
        var result = await _mediator.Send(new CreateOrderItemCommand(orderId, request.ProductId, request.Quantity));

        if (result.HasError)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result.Data);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateOrderItemRequest request)
    {
        var result = await _mediator.Send(new UpdateOrderItemCommand(id, request.ProductId, request.Quantity));

        if (result.HasError)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result.Data);
    }
}