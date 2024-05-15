namespace starter_serv.ViewModel
{
    public class PagedRepositoryResponse<T>
    {
        public PagedInfoRepositoryResponse Info { get; set; }
        public T Results { get; set; }
    }
    public class PagedInfoRepositoryResponse
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int Length { get; set; }
    }
}
