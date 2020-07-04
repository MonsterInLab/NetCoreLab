using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Center.Web.Utility
{
    public class SecondMiddleWare
    {
        private readonly RequestDelegate _next;

        public SecondMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync($"{nameof(SecondMiddleWare)},Hello World1!<br/>");
            await _next(context);
            await context.Response.WriteAsync($"{nameof(SecondMiddleWare)},Hello World2!<br/>");
        }
    }
}
