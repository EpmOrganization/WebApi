using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPM.WebApi.Filter
{
    //public class CustomerResultFilter : IAsyncResultFilter
    //{

    //    public   Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    //    {

    //        long? length = context.HttpContext.Response.ContentLength;

    //        //Object obj = await context.Result.ExecuteResultAsync(context);


    //        //byte[] buffer = new byte[(int)context.HttpContext.Response..ContentLength];
    //        // context.HttpContext.Response.Body.ReadAsync(buffer, 0, (int)context.HttpContext.Response.ContentLength);
    //        //string j = Encoding.UTF8.GetString(buffer);

    //        //Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(j));

    //        return Task.CompletedTask;
    //    }
    //}

    public class CustomerResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            long? length = context.HttpContext.Response.ContentLength;
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
           
        }

        public Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {

            long? length = context.HttpContext.Response.ContentLength;

            //Object obj = await context.Result.ExecuteResultAsync(context);


            //byte[] buffer = new byte[(int)context.HttpContext.Response..ContentLength];
            // context.HttpContext.Response.Body.ReadAsync(buffer, 0, (int)context.HttpContext.Response.ContentLength);
            //string j = Encoding.UTF8.GetString(buffer);

            //Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(j));

            return Task.CompletedTask;
        }
    }
}
