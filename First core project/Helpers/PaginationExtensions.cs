using Microsoft.EntityFrameworkCore;

namespace First_core_project.Helpers
{
    public static class PaginationExtensions
    {
        public static IQueryable<T> ApplyPagination<T>(
            this IQueryable<T> query,
            PaginationParams param)
        {
            // Sorting
            if (!string.IsNullOrEmpty(param.SortBy))
            {
                query = param.Desc
                    ? query.OrderByDescending(e =>
                        EF.Property<object>(e, param.SortBy))
                    : query.OrderBy(e =>
                        EF.Property<object>(e, param.SortBy));
            }

            // Pagination
            return query
                .Skip((param.PageNumber - 1) * param.PageSize)
                .Take(param.PageSize);
        }
    }
}