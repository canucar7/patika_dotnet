using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;
using WebApi.Services;

namespace WebApi.Middlewares
{

    public class CustomExceptionMiddleware
    {

        private readonly RequestDelegate _next;

        private readonly ILoggerService _loggerServices;
        
        public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerServices)
        {
            _next = next;
            _loggerServices = loggerServices;
        }


        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            try
            {
                string message = "[Request] HTTP " + context.Request.Method + " - " + context.Request.Path;
                _loggerServices.Write(message);

                await _next(context);
                watch.Stop();

                message = "[Request] HTTP " + context.Request.Method + " - " + context.Request.Path + " responded " + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds+" ms";  
                _loggerServices.Write(message);  

                
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleException(context,ex,watch);
            }
        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {

            context.Response.ContentType="application/json";
            context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;

            string message = "[Error]   HTTP " + context.Request.Method + " - " + context.Response.StatusCode + " Error Message " + ex.Message + " in " + watch.Elapsed.TotalMilliseconds+" ms";
            _loggerServices.Write(message);

  
            var result = JsonConvert.SerializeObject(new {error=ex.Message},Formatting.None);
            
            return context.Response.WriteAsync(result);
        }    
        
    }

    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }


}   