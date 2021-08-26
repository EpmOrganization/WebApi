using EPM.WebApi;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EPM.XUnitTest
{
    /// <summary>
    /// 基类，返回HttpClient对象
    /// </summary>
    public class ApiControllerTestBase
    {
        protected HttpClient GetClient()
        {
            var server = new TestServer(WebHost.CreateDefaultBuilder()
            .UseStartup<Startup>());
            // 创建HttpClient
            HttpClient client = server.CreateClient();

            return client;
        }
    }
}
