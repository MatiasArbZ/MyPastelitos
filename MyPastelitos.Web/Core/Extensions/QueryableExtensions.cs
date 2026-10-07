using MyPastelitos.Web.Core.Pagination;
using System.Runtime.CompilerServices;  

namespace MyPastelitos.Web.Core.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> PaginateAsync<T>(this IQueryable<T> queryable, PaginationRequest request)
        {
            return queryable.Skip((request.Page - 1) * request.RecordPerPage)
                            .Take(request.RecordPerPage);
        }
    }
}
