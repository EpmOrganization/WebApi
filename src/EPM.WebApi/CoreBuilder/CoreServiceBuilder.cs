using EPM.EFCore.Context;
using EPM.Model.ConfigModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.WebApi.CoreBuilder
{
    public class CoreServiceBuilder : ICoreServiceBuilder
    {
        private readonly IServiceCollection _services;
        private readonly IConfiguration _configuration;

        // 全局变量，后面的名字可以随意
        //readonly string CustomerSpecificOrigins = "CustomerSpecificOrigins";

        public CoreServiceBuilder(IServiceCollection service, IConfiguration configuration)
        {
            _services = service;
            _configuration = configuration;
        }

        public void AddDbConfig()
        {
            // 获取配置信息
            ConnectionStrings connectionStrings = Appsettings.Instance().ConnectionStrings;
            var serverVersion = new MySqlServerVersion(new Version(connectionStrings.Major, connectionStrings.Minor, connectionStrings.Build));
            //添加数据上下文
            _services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql(connectionString: connectionStrings.DbConnection,serverVersion:serverVersion);
            });
        }
        public void AddSwaggerGenerator()
        {
            _services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EPM.WebApi", Version = "v1" });
            });
        }
    }
}
