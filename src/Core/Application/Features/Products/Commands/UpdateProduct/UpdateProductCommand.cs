using Application.Common;
using Application.Responses;
using MediatR;

namespace Application.Features.Products.Commands.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, double Price) : IRequest<Result<ProductResponse>>;