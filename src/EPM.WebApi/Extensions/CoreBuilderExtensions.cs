using EPM.WebApi.CoreBuilder;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.WebApi.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class CoreBuilderExtensions
    {
        /// <summary>
        /// 注册服务到依赖注入容器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddCoreServiceProvider(this IServiceCollection services, IConfiguration configuration)
        {
            // 实例化
            ICoreServiceBuilder servicebuilder = new CoreServiceBuilder(services, configuration);
            // 添加数据库
            servicebuilder.AddDbConfig();
            // 添加Swagger
            servicebuilder.AddSwaggerGenerator();

            servicebuilder.AddCORS();
        }


        /// <summary>
        /// 添加配置
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        public static void AddCoreConfigureProvider(this IApplicationBuilder app, IConfiguration configuration)
        {
            ICoreConfigurationBuilder configurationBuilder = new CoreConfigureBuilder(app, configuration);
            configurationBuilder.UseSwagger();


        }
    }
}
