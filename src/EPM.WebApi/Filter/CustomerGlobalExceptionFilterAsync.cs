using EPM.Framework.Helper;
using EPM.Model.ApiModel;
using EPM.WebApi.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.WebApi.Filter
{
    public class CustomerGlobalExceptionFilterAsync : IAsyncExceptionFilter
    {
        private readonly ILogger<CustomerGlobalExceptionFilterAsync> _logger;

        public CustomerGlobalExceptionFilterAsync(ILogger<CustomerGlobalExceptionFilterAsync> logger)
        {
               _logger = logger;   
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            // 如果异常没有被处理，则进行处理
            if (context.ExceptionHandled == false)
            {
                // 可以把日志存入数据库或者ES中
                _logger.LogDebug("ces");

                ApiResponse apiResponse=new ApiResponse();
                apiResponse.Code = 0;
                if (context.Exception.InnerException != null)
                    apiResponse.Msg = context.Exception.InnerException.Message;
                else
                    apiResponse.Msg = context.Exception.Message;

                _logger.LogDebug(apiResponse.Msg);
                context.Result = new ContentResult
                {

                    Content = JsonConvert.SerializeObject(apiResponse),
                    // 返回状态设置为200,表示成功
                    StatusCode = StatusCodes.Status200OK,
                    // 设置返回json格式
                    ContentType = "application/json;charset=utf-8"
                };
                // 设置为true，表示异常已经被处理了，其它捕获异常的地方就不会再处理了
                context.ExceptionHandled = true;
            }

            return Task.CompletedTask;
        }
    }
}
