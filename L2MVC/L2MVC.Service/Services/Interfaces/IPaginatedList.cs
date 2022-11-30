namespace L2MVC.Service.Services.Interfaces
{
    public interface IPaginatedList<T> : IList<T>
    {
        bool HasNextPage { get; }
        bool HasPreviousPage { get; }
        int PageIndex { get; }
        int TotalPages { get; }
        int PageSize { get; }
        int TotalItems { get; }
    }
}