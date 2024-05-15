using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using starter_serv.ViewModel;

namespace starter_serv.Helper
{

    public class UnauthorizedMiddleware
    {
        private readonly RequestDelegate _next;

        public UnauthorizedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                context.Response.ContentType = "application/json";
                var response = new 
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Message = "Unauthorized",
                    Data = new { },
                    Error = new
                    {
                        Unauthorized = "API is not allowed to access"
                    }
                };
                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        }
    }


}
