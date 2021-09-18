using EPM.EFCore.Context;
using EPM.EFCore.Uow;
using EPM.Framework.Extensions;
using EPM.Framework.Settings;
using EPM.Model.ConfigModel;
using EPM.WebApi.Extensions;
using EPM.WebApi.Filter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

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

            // 过滤器
            //services.AddControllers(options => options.Filters.Add(typeof(CustomerResultFilter)));
            services.AddControllers(options => options.Filters.Add(typeof(CustomerGlobalExceptionFilterAsync)));

            #region 配置JWT验证
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
            JwtConfig jwtConfig = Configuration.GetSection("JwtConfig").Get<JwtConfig>();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => 
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.IssuerSigningKey)),
                    ValidIssuer = jwtConfig.Issuer,
                    ValidAudience = jwtConfig.Audience,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    // 设置缓冲时间为0
                    ClockSkew = TimeSpan.Zero,
                    RequireExpirationTime = true
                };
            });
            #endregion

            #region 扩展方法
            services.AddCoreServiceProvider(Configuration);
            #endregion

            #region 批量注入
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

            #region 添加身份认证中间件
            app.UseAuthentication(); 
            #endregion

            app.UseRouting();

            #region 添加跨域中间件 需要设置再UseRouting和UseAuthorization之间

            app.UseCors(CustomerSpecificOrigins);
            #endregion

            app.UseAuthorization();

            //app.UseMiddleware(typeof(CustomerTestMiddleware));

            app.UseEndpoints(endpoints =>
            {
                //跨域需添加RequireCors方法，CustomerSpecificOrigins是再ConfigureServices方法中配置的跨域策略名称
                endpoints.MapControllers().RequireCors(CustomerSpecificOrigins);
            });
        }
    }
}
