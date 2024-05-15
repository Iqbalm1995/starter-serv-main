namespace starter_serv.ViewModel
{
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PagedInfoRepositoryResponse Info { get; set; }
        public PagedResponse()
        {
            this.PageNumber = 0;
            this.PageSize = 0;
            this.Data = default(T);
            this.Message = "";
            this.Succeeded = false;
            this.Errors = null;
        }
        public PagedResponse(string message)
        {
            this.PageNumber = 0;
            this.PageSize = 0;
            this.Data = default(T);
            this.Message = message;
            this.Succeeded = false;
            this.Errors = null;
        }
        public PagedResponse(T data, int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }
        public PagedResponse(T data, PagedInfoRepositoryResponse info, int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
            this.Info = info;
        }
    }
}
