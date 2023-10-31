using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
namespace vphone.Middleware
{
    public class CheckAdmin
    {
        private readonly RequestDelegate _next;

        public CheckAdmin(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Session.TryGetValue("account", out byte[] value))
            {
                context.Response.Redirect("/admin/login");
                return;
            }

            await _next(context);
        }
    }
}
