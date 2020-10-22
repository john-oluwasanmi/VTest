using System;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace V.Test.Web.App.Filter
{
    public class ExceptionHandlerFilterAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var logger = (ILogger)filterContext?.HttpContext?.RequestServices?.GetService(typeof(ILogger));

            var exceptionMessage = filterContext?.Exception?.Message;
            var exceptionStackTrack = filterContext?.Exception?.StackTrace;
            var innerException = filterContext?.Exception?.InnerException?.Message;
            var controllerName = filterContext?.RouteData?.Values["controller"]?.ToString();
            var actionName = filterContext?.RouteData?.Values["action"]?.ToString();
            var exceptionLogTime = DateTime.UtcNow;

            var execption = JsonConvert.SerializeObject(filterContext?.Exception, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            logger.LogError($"Message : {exceptionMessage}, StackTrack : {exceptionStackTrack} ,Controller : {controllerName}, Action : {actionName}, Inner Exception : {innerException} , Time : {exceptionLogTime}, exception : {execption} ");

            string url = $"~/Home/Error";
            filterContext.Result = new RedirectResult(url);
            filterContext.ExceptionHandled = true;

            return;
        }
    }
}
