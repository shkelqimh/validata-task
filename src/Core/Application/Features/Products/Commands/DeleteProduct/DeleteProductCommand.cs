using Application.Common;
using Application.Responses;
using MediatR;

namespace Application.Features.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest<Result<ProductResponse>>;