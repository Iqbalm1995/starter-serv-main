using starter_serv.ViewModel;

namespace starter_serv.Helper
{
    public interface IRequestParameter
    {
        int Page { get; set; }
        int Length { get; set; }
        List<string> Orders { get; set; }
        string SortType { get; set; }
        List<RequestFilterParameter> Filters { get; set; }
    }
}
