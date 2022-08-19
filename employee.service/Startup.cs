using employee.service.Configurations;
using employee.service.Filters;
using employee.service.Logger;
using employee.service.Repositories.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace employee.service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //// This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    var appSettings = new AppSettings();
        //    ConfigurationBinder.Bind(Configuration.GetSection("AppSettings"), appSettings);
        //    services.AddSingleton<IAppSettings>(appSettings);
        //    services.AddSingleton<CustomAuthorize>();
        //    services.AddSingleton<IDatabaseRepository, DatabaseRepository>();


        //    //var loggerFactory = new LoggerFactory();
        //    //loggerFactory
        //    //   .WithFilter(new FilterLoggerSettings
        //    //   {
        //    //        { "Microsoft", LogLevel.None },
        //    //        { "System", Microsoft.Extensions.Logging.LogLevel.Warning },
        //    //        { "HyperLogFilter", Microsoft.Extensions.Logging.LogLevel.Trace }
        //    //   })
        //    //   .AddConsole();
        //    //loggerFactory.AddDebug();

        //    //services.AddMvc(_ =>
        //    //{
        //    //    _.Filters.Add(new CustomExceptionFilter(appSettings, loggerFactory));
        //    //});

        //    services.AddControllers();
        //}

        //// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        //{
        //    //loggerFactory
        //    //   .WithFilter(new FilterLoggerSettings
        //    //   {
        //    //        { "Microsoft",LogLevel.None },
        //    //        { "System", LogLevel.Warning },
        //    //        { "HyperLogFilter", LogLevel.Trace }
        //    //   })
        //    //   .AddConsole();
        //    //loggerFactory.AddDebug();

        //    app.UseMiddleware<LogResponseMiddleware>();
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }

        //    //_ = app.UseMvc();

        //    app.UseRouting();

        //    app.UseEndpoints(endpoints =>
        //    {
        //        endpoints.MapControllers();
        //    });
        //}






        #region Auto generated code
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("doc", new OpenApiInfo { Title = "Core Microservice" });
                opt.AddSecurityDefinition("apikey", new OpenApiSecurityScheme { In = ParameterLocation.Header, Description = "Please pass api key" });
                //opt.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                //    {"apiKey", Enumerable.Empty<string>() }
                //});
            });

            var appSettings = new AppSettings();
            ConfigurationBinder.Bind(Configuration.GetSection("AppSettings"), appSettings);
            services.AddSingleton<CustomExceptionFilter>();
            services.AddSingleton(appSettings);

            services.AddSingleton<CustomAuthorize>();
            services.AddSingleton<IDatabaseRepository, DatabaseRepository>();

            services.AddControllers();

            //services.AddMvc(_ =>
            //{
            //    _.Filters.Add(new CustomExceptionFilter(appSettings));
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("doc/swagger.json", "Core Service API");
            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        #endregion

    }
}
