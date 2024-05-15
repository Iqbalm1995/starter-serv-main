using System.Diagnostics.CodeAnalysis;

namespace starter_serv.ViewModel
{
    [ExcludeFromCodeCoverage]
    public class ResponseOneDataViewModel<T> where T : class
    {
        public int StatusCode { set; get; }
        public string Message { set; get; }
        public T Data { set; get; }
    }
}
