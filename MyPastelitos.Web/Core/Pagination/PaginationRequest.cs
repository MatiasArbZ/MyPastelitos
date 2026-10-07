namespace MyPastelitos.Web.Core.Pagination
{
    public class PaginationRequest
    {
        private int _page = 1;
        private int _recordPerPage = 15;
        private const int MAX_RECORDS_PER_PAGE = 50;

        public string? Filter { get; set; }
        public int Page 
        { get => _page; 
          set => _page = value > 0 ? value : 1; 
        }
        public int RecordPerPage 
        { get => _recordPerPage; 
          set => _recordPerPage = value <= MAX_RECORDS_PER_PAGE ? value : MAX_RECORDS_PER_PAGE; 
        }

        public static PaginationRequest Default => new PaginationRequest { Page = 1, RecordPerPage = 15 };
    }
}
