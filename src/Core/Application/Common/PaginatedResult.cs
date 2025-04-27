namespace Application.Common;

public class PaginatedResult<T> where T : class
{
    public T Data { get; private init; }
    public int TotalCount { get; private init; }
    public int PageNumber { get; private init; }
    public int PageSize { get; private init; }
    public int TotalPages { get; private init; }
    
    public static PaginatedResult<T> Success(T data, int pageNumber, int pageSize, int totalCount)
        => new ()
        {
            Data = data,
            PageNumber = pageNumber,
            TotalCount = totalCount,
            PageSize = pageSize,
            TotalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / (double)pageSize))
        };
}