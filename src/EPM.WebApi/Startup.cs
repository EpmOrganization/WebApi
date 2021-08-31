using EPM.EFCore.Context;
using EPM.EFCore.Uow;
using EPM.Framework.Extensions;
using EPM.Model.ConfigModel;
using EPM.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace EPM.WebApi
{
    public class Startup
    {
        // 全局变量，后面的名字可以随意
        readonly string CustomerSpecificOrigins = "CustomerSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region 初始化配置信息
            Appsettings.Instance().Initaial(Configuration);
            #endregion

            services.AddControllers();


            #region 扩展方法
            services.AddCoreServiceProvider(Configuration);
            #endregion

            #region 批量注入
            services.RegisterService(ServiceLifetime.Scoped, new string[] { "EPM.Repository", "EPM.Service" });
            services.AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EPM.WebApi v1"));
            }

            app.AddCoreConfigureProvider(Configuration);
            app.UseHttpsRedirection();

            app.UseRouting();

            #region 添加跨域中间件 需要设置再UseRouting和UseAuthorization之间

            app.UseCors(CustomerSpecificOrigins);
            #endregion

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //跨域需添加RequireCors方法，CustomerSpecificOrigins是再ConfigureServices方法中配置的跨域策略名称
                endpoints.MapControllers().RequireCors(CustomerSpecificOrigins);
            });
        }
    }
}
