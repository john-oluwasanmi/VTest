using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace V.Test.Web.App.Filter
{
    public class ActionFilterAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Do something before the action executes.
            //   MyDebug.Write(MethodBase.GetCurrentMethod(), context.HttpContext.Request.Path);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do something after the action executes.
            // MyDebug.Write(MethodBase.GetCurrentMethod(), context.HttpContext.Request.Path);
        }
    }
}
