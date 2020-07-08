using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Center.Web.Utility
{
    /// <summary>
    /// 服务器缓存
    /// </summary>
    public class CustomResourceFilterAttribute : Attribute, IResourceFilter, IFilterMetadata
    {
        private static Dictionary<string, IActionResult> CustomCache = new Dictionary<string, IActionResult>();

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine($"This is {nameof(CustomResourceFilterAttribute) }OnResourceExecuting");
            //获取缓存
            string key = context.HttpContext.Request.Path;
            if (CustomCache.ContainsKey(key))
            {
                context.Result = CustomCache[key];
            }

        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine($"This is {nameof(CustomResourceFilterAttribute) }OnResourceExecuted");
            //执行缓存
            string key = context.HttpContext.Request.Path;
            if (CustomCache.ContainsKey(key))
            {
                CustomCache.Add(key, context.Result);
            }
        }


    }
}
