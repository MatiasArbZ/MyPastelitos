namespace MyPastelitos.Web.Core.Pagination
{
    public class PaginationResponse<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int RecordsPerPage{ get; set; }
        public string? Filter { get; set; }
        public int TotalCount  { get; set; }
        public PagedList<T> List { get; set; }
        

        public bool HasPrevius => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

    }
}
