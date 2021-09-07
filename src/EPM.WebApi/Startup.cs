using EPM.EFCore.Context;
using EPM.EFCore.Uow;
using EPM.Framework.Extensions;
using EPM.Framework.Settings;
using EPM.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EPM.WebApi
{
    public class Startup
    {
        // ȫ�ֱ�������������ֿ�������
        readonly string CustomerSpecificOrigins = "CustomerSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region ��ʼ��������Ϣ
            Appsettings.Instance().Initaial(Configuration);
            #endregion

            services.AddControllers();

            // ������
            //services.AddControllers(options => options.Filters.Add(typeof(CustomerResultFilter)));

            #region ��չ����
            services.AddCoreServiceProvider(Configuration);
            #endregion

            #region ����ע��
            services.RegisterService(ServiceLifetime.Scoped, new string[] { "EPM.Repository", "EPM.Service" });
            services.AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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

            #region ��ӿ����м�� ��Ҫ������UseRouting��UseAuthorization֮��

            app.UseCors(CustomerSpecificOrigins);
            #endregion

            app.UseAuthorization();

            //app.UseMiddleware(typeof(CustomerTestMiddleware));

            app.UseEndpoints(endpoints =>
            {
                //���������RequireCors������CustomerSpecificOrigins����ConfigureServices���������õĿ����������
                endpoints.MapControllers().RequireCors(CustomerSpecificOrigins);
            });
        }
    }
}
