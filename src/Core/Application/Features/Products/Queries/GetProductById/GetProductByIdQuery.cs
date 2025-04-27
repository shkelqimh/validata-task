using Application.Common;
using Application.Responses;
using MediatR;

namespace Application.Features.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<Result<ProductResponse>>;