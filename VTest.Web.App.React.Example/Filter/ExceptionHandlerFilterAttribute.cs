using System;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace V.Test.Web.App.Filter
{
    public class ExceptionHandlerFilterAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            //var logger = ( )filterContext?.HttpContext?.RequestServices?.GetService(typeof( ));

            var exceptionMessage = filterContext?.Exception?.Message;
            var exceptionStackTrack = filterContext?.Exception?.StackTrace;
            var innerException = filterContext?.Exception?.InnerException?.Message;
            var controllerName = filterContext?.RouteData?.Values["controller"]?.ToString();
            var actionName = filterContext?.RouteData?.Values["action"]?.ToString();
            var exceptionLogTime = DateTime.UtcNow;

          //  var execption = JsonConvert.SerializeObject(filterContext?.Exception);

            //logger.Error($"Message : {exceptionMessage}, StackTrack : {exceptionStackTrack}" +
            //             $",Controller : {controllerName}, Action : {actionName}, Inner Exception : {innerException}" +
            //             $", Time : {exceptionLogTime}, exception : {execption} ");

            filterContext.ExceptionHandled = true;
            var brandName = filterContext?.RouteData?.Values["brandName"]?.ToString();

            string url = $"~/Home/Error";
            filterContext.Result = new RedirectResult(url);
            filterContext.ExceptionHandled = true;

            return;
        }
    }
}
