using Application.Responses;
using Domain.Entities;

namespace Application.Mappings;

internal static class ProductMappings
{
    public static ProductResponse ToProductResponse(this Product product)
    {
        return new ProductResponse(product.Id, product.Name, product.Price, product.CreatedOn, product.ModifiedOn);
    }
}