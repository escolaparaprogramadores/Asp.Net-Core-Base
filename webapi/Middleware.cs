using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using webapi;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Newtonsoft.Json;

namespace webapi
{
    public class Middleware 
    {
        private readonly RequestDelegate _next;

        public Middleware(RequestDelegate next)
        {
            _next = next;
        }

        async public Task  Invoke(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json"; 
            await _next(httpContext);

            if(httpContext.Response.StatusCode == 404)
            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new{statusCode = 404, message = "Método não encontrado"})); 

        }
        
    }
}
public static class MiddlewareExtensions
{
  public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
  {
    return builder.UseMiddleware<Middleware>();
  }
}