using L2MVC.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace L2MVC.Service.Services
{
    public class PaginatedList<T> : List<T>, IPaginatedList<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; set; }
        public int TotalItems { get; private set; }

        public PaginatedList(IEnumerable<T> items, int count, int pageIndex, int pageSize)
        {
            TotalItems = count;
            PageSize = pageSize;
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}