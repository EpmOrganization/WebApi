using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.WebApi
{
    public class CustomerTestMiddleware
    {
        /// <summary>
        /// 委托
        /// </summary>
        private readonly RequestDelegate _next;


        public CustomerTestMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            long? length1 = httpContext.Request.ContentLength;
            long? length= httpContext.Response.ContentLength;

            Console.WriteLine("执行前");
            await _next(httpContext);
            Console.WriteLine("执行后");
            long? length2 = httpContext.Response.ContentLength;
            long? len = length2 == null ? -1 : length2;

            //byte[] buffer = new byte[(int)Request.ContentLength];
            //await Request.Body.ReadAsync(buffer, 0, (int)Request.ContentLength);
            //string j = Encoding.UTF8.GetString(buffer);

            //Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(j));

            //httpContext.Response.Body.ReadAsync()

           // httpContext.Response.
            Console.WriteLine($"执行后长度:{len}");
        }
    }
}
