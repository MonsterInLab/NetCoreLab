using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Center.Web.Utility
{
    public class ThreeMiddleWare
    {
        private readonly RequestDelegate _next;

        public ThreeMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value.Contains("Eleven"))
                await context.Response.WriteAsync($"{nameof(ThreeMiddleWare)}This is End<br/>");
            else
            {
                await context.Response.WriteAsync($"{nameof(ThreeMiddleWare)},Hello World ThreeMiddleWare!<br/>");
                await _next(context);
                await context.Response.WriteAsync($"{nameof(ThreeMiddleWare)},Hello World ThreeMiddleWare!<br/>");
            }
        }
    }
}
