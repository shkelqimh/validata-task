using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Commands.DeleteProduct;
using Application.Features.Products.Commands.UpdateProduct;
using Application.Features.Products.Queries.GetProductById;
using Application.Features.Products.Queries.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Requests;

namespace WebApi.Controllers;

[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(PaginationRequest request)
    {
        return Ok(await _mediator.Send(new GetProductsQuery(request.PageNumber, request.PageSize)));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(id));

        if (result.HasError)
        {
            return NotFound(result.Errors);
        }

        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
    {
        var result = await _mediator.Send(new CreateProductCommand(request.Name, request.Price));

        if (result.HasError)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result.Data);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProductRequest request)
    {
        var result = await _mediator.Send(new UpdateProductCommand(id, request.Name, request.Price));

        if (result.HasError)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result.Data);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new DeleteProductCommand(id));

        if (result.HasError)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }
}