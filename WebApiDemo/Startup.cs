using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.CustomerMiddlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApiDemo.DataAccess;
using WebApiDemo.Formatters;

namespace WebApiDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options=> 
            {
                options.OutputFormatters.Add(new VcardOutputFormatter());
            });

            services.AddScoped<IProductDal, EfProductDal>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggerFactory loggerFactory)
        {
            //Middleware'ler yazılan sırayla çalışırlar.
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
           
            app.UseMiddleware<AuthenticationMiddleware>();
            app.UseMvc();

            //app.UseMvc(config =>
            //{

            //    config.MapRoute("DefaultRoute", "api/{controller}/{action}");
            //});
        }
    }
}
