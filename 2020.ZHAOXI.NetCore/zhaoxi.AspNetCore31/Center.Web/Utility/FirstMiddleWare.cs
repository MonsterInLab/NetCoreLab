using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Center.Web.Utility
{
    public class FirstMiddleWare
    {
        private readonly RequestDelegate _next;

        public FirstMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync($"{nameof(FirstMiddleWare)},Hello World start!<br/>");
            await _next(context);
            await context.Response.WriteAsync($"{nameof(FirstMiddleWare)},Hellow world end!");
        }
    }
}
