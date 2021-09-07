using EPM.EFCore.Context;
using EPM.Framework.Data;
using EPM.Framework.Settings;
using EPM.Model.ConfigModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace EPM.WebApi.CoreBuilder
{
    public class CoreServiceBuilder : ICoreServiceBuilder
    {
        private readonly IServiceCollection _services;
        private readonly IConfiguration _configuration;

        // 全局变量，后面的名字可以随意
        readonly string CustomerSpecificOrigins = "CustomerSpecificOrigins";

        public CoreServiceBuilder(IServiceCollection service, IConfiguration configuration)
        {
            _services = service;
            _configuration = configuration;
        }

        public void AddCORS()
        {
            // 添加跨域服务
            _services.AddCors(options =>
            {
                //配置跨域处理，允许所有来源
                options.AddPolicy(CustomerSpecificOrigins,
                 builder => builder.AllowAnyOrigin()
                 .AllowAnyHeader()
                 .WithExposedHeaders("Content-Disposition")
                 .AllowAnyMethod());
            });
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

        public void AddOther()
        {
            #region 解决返回时间带T的问题
            _services.AddControllers().AddJsonOptions(configure =>
            {
                configure.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter());

            });
            #endregion
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
