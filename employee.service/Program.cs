using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Compact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace employee.service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .UseSerilog()
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
               })
              .ConfigureServices((hostContext, services) =>
              {
                  var configuration = hostContext.Configuration;
                  LogConfiguration(configuration);
              });


        private static void LogConfiguration(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                //.WriteTo.Console(new RenderedCompactJsonFormatter())
                .WriteTo.Console()
                .WriteTo.Debug(outputTemplate: DateTime.Now.ToString())
                //.WriteTo.File($"{configuration.GetValue<string>("LogPath")}log.txt", rollingInterval: RollingInterval.Day)
               .CreateLogger();
        }
    }
}
