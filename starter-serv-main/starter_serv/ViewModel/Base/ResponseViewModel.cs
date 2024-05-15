using System.Diagnostics.CodeAnalysis;

namespace starter_serv.ViewModel
{
    [ExcludeFromCodeCoverage]
    public class ResponseViewModel<T>
    {
        public int StatusCode { set; get; }
        public string Message { set; get; }
        public int Count { set; get; }
        public int CountTotal { set; get; }
        public List<T> Data { set; get; }
    }
}
