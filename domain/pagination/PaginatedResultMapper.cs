namespace domain.pagination;

public class PaginatedResultMapper
{
    /// <summary>
    /// Maps the items of a PaginatedResult<TSource> to a new PaginatedResult<TDestination> using the provided mapping function.
    /// </summary>
    /// <typeparam name="TSource">The source type of the items.</typeparam>
    /// <typeparam name="TDestination">The destination type of the items.</typeparam>
    /// <param name="source">The original PaginatedResult with items of type TSource.</param>
    /// <param name="mapFunction">A function to map each item from TSource to TDestination.</param>
    /// <returns>A new PaginatedResult with items of type TDestination.</returns>
    public static PaginatedResult<TDestination> Map<TSource, TDestination>(
        PaginatedResult<TSource> source,
        Func<TSource, TDestination> mapFunction)
    {
        return new PaginatedResult<TDestination>
        {
            Items = source.Items.Select(mapFunction).ToList(),
            TotalCount = source.TotalCount,
            PageNumber = source.PageNumber,
            PageSize = source.PageSize,
            TotalPages = source.TotalPages
        };
    }
}