using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using log4net.Repository.Hierarchy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;

namespace Center.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILoggerFactory loggerFactory)
        {

            //app.Run(context =>
            //{
            //   return context.Response.WriteAsync("This is hello word");
            //});

            #region ÖÐ¼ä¼þ RequestDelegate
            app.Use(next =>
            {
                Console.WriteLine("Middleware 1 ");
                return new RequestDelegate(
                    async context =>
                    {
                        await context.Response.WriteAsync("This IS Hello word 1 start<br/>");
                        await next.Invoke(context);
                        await context.Response.WriteAsync("This is hello word 1 end<br/>");
                    }
                    );
            });
            app.Use(next =>
            {
                Console.WriteLine("Middleware 2 ");
                return new RequestDelegate(
                    async context =>
                    {
                        await context.Response.WriteAsync("This IS Hello word 2 start<br/>");
                        await next.Invoke(context);
                        await context.Response.WriteAsync("This is hello word 2 end<br/>");
                    }
                    );
            });
            app.Use(next =>
            {
                Console.WriteLine("Middleware 3 ");
                return new RequestDelegate(
                    async context =>
                    {
                        await context.Response.WriteAsync("This IS Hello word 3 start");
                       // await next.Invoke(context);
                        await context.Response.WriteAsync("This is hello word 3 end");
                    }
                    );
            });

            #endregion

            #region  app config
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot"))
            //});

            //loggerFactory.AddLog4Net();

            //app.UseSession();

            //app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});
            #endregion
        }
    }
}
