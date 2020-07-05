using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Center.Web.Utility
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IModelMetadataProvider _modelMetadataProvider;
        private readonly ILogger<CustomExceptionFilterAttribute> _logger;

        public CustomExceptionFilterAttribute(ILogger<CustomExceptionFilterAttribute> logger,
            IModelMetadataProvider modelMetadataProvider
                    )
        {
            this._modelMetadataProvider = modelMetadataProvider;
             this._logger = logger;
        }

        /// <summary>
        /// 打印异常
        /// 信息日志
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            #region 中断式
            //if (!context.ExceptionHandled)
            //{
            //    context.Result = new JsonResult(new
            //    {
            //        Result = false,
            //        Msg = context.Exception.Message
            //    }); //中断式--请求到这里久结束了，不再继续Action
            //    context.ExceptionHandled = true;
            //}
            #endregion

            this._logger.LogError($"{context.HttpContext.Request.RouteValues["controller"]} is Error");

            if (this.IsAjaxRequest(context.HttpContext.Request))
            {
                context.Result = new JsonResult(new
                {
                    Result = false,
                    Msg = context.Exception.Message
                });
            }
            else
            {
                var result = new ViewResult { ViewName = "~/Views/Shared/Error.cshtml" };
                 result.ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState);
                result.ViewData.Add("Exception", context.Exception);
                context.Result = result;
            }
            context.ExceptionHandled = true;

            base.OnException(context);
        }

        private bool IsAjaxRequest(HttpRequest request)
        {
            string header = request.Headers["X-Requested-With"];
            return "XMLHttpRequest".Equals(header);
        }
    }
}
