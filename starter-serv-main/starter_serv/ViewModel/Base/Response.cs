using System.Net;

namespace starter_serv.ViewModel
{
    public class Response<T>
    {
        public Response()
        {
            Succeeded = false;
            Message = "Unknown Error";
        }
        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
            StatusCode = (int)HttpStatusCode.OK;
        }
        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public String ErrorCode { get; set; }
        public T Data { get; set; }
        public int StatusCode { get; set; }
    }
}
