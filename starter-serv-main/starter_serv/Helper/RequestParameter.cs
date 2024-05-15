using starter_serv.ViewModel;

namespace starter_serv.Helper
{
    public class RequestParameter
    {
        public int Page { get; set; }
        public int Length { get; set; }
        public List<string> Orders { get; set; }
        public string SortType { get; set; }
        public List<RequestFilterParameter> Filters { get; set; }
        public RequestParameter()
        {
            this.Page = 0;
            this.Length = 0;
            this.Orders = new List<string>();
            this.SortType = "ASC";
            this.Filters = new List<RequestFilterParameter>();
        }
        public RequestParameter(int page, int length)
        {
            this.Page = page < 1 ? 1 : page;
            this.Length = length > 50 ? 50 : length;
            this.Orders = new List<string>();
            this.SortType = "ASC";
            this.Filters = new List<RequestFilterParameter>();
        }
        public int CalculateOffset()
        {
            return (Page - 1) == 0 || (Page - 1) < 0 ? 0 : (Page - 1) * Length;
        }
    }
}
