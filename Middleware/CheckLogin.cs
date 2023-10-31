using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vphone.Models;
using System.Text;

namespace vphone.Middleware
{
    public class CheckLogin
    {
        private readonly RequestDelegate _next;

        public CheckLogin(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Session.TryGetValue("account", out byte[] value))
            {
                context.Response.Redirect("/admin/dashboard");
                return;
            }
            await _next(context);
        }
    }
}
