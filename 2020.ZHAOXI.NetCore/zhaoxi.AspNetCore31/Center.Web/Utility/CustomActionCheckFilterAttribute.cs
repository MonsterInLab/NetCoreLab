using Center.Web.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Center.Web.Utility.WebHelper;

namespace Center.Web.Utility
{
    public class CustomActionCheckFilterAttribute : ActionFilterAttribute
    {
        #region Identity
        private readonly ILogger<CustomActionCheckFilterAttribute> _logger;
        private readonly IModelMetadataProvider _modelMetadataProvider;
        public CustomActionCheckFilterAttribute(ILogger<CustomActionCheckFilterAttribute> logger
            )
        {
            this._logger = logger;
        }
        #endregion

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            CurrentUser currentUser = context.HttpContext.GetCurrentUserBySession();
            if (currentUser == null)
            {
                context.Result = new RedirectResult("~/Fourth/Login");
            }
            else
            {
                var path = context.HttpContext.Request.Path;
                this._logger.LogDebug($"{currentUser.Name} 访问系统:{path} ");
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

    }
}
