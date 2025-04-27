using Application.Common;
using Application.Responses;
using MediatR;

namespace Application.Features.Products.Commands.CreateProduct;

public record CreateProductCommand(string Name, double Price) : IRequest<Result<ProductResponse>>;