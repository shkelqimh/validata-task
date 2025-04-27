namespace Shared.ExtensionMethods;

public static class CollectionExtensions
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> list, int pageNumber, int pageSize) where T : class
        => list?.Skip((pageNumber - 1) * pageSize).Take(pageSize);
}