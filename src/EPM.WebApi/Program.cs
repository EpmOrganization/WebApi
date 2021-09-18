using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // ��ȡ����
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json",optional: true,reloadOnChange:true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                // ��ȡ�����ļ�
                .ReadFrom.Configuration(configuration)
                 .WriteTo.File($"{configuration.GetSection("LogPath").Value}",
                rollingInterval: RollingInterval.Day,
                rollOnFileSizeLimit: true)
                // ����
                .CreateLogger();

            try
            {
                Log.Information("Starting host...");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                // ʹ��Serilog
                .UseSerilog();
    }
}
