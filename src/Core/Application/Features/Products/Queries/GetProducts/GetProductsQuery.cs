using Application.Common;
using Application.Responses;
using MediatR;

namespace Application.Features.Products.Queries.GetProducts;

public record GetProductsQuery(int PageNumber, int PageSize) : IRequest<PaginatedResult<List<ProductResponse>>>;
