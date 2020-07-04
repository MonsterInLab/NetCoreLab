using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Center.Web.Utility;
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

            #region Use中间件 
            //终结时
            //app.Run(context =>
            //{
            //   return context.Response.WriteAsync("This is hello word");
            //});

            //app.Use(next =>
            //{
            //    Console.WriteLine("Middleware 1 ");
            //    return new RequestDelegate(
            //        async context =>
            //        {
            //            await context.Response.WriteAsync("This IS Hello word 1 start<br/>");
            //            await next.Invoke(context);
            //            await context.Response.WriteAsync("This is hello word 1 end<br/>");
            //        }
            //        );
            //});
            //app.Use(next =>
            //{
            //    Console.WriteLine("Middleware 2 ");
            //    return new RequestDelegate(
            //        async context =>
            //        {
            //            await context.Response.WriteAsync("This IS Hello word 2 start<br/>");
            //            await next.Invoke(context);
            //            await context.Response.WriteAsync("This is hello word 2 end<br/>");
            //        }
            //        );
            //});
            //app.Use(next =>
            //{
            //    Console.WriteLine("Middleware 3 ");
            //    return new RequestDelegate(
            //        async context =>
            //        {
            //            await context.Response.WriteAsync("This IS Hello word 3 start");
            //           // await next.Invoke(context);
            //            await context.Response.WriteAsync("This is hello word 3 end");
            //        }
            //        );
            //});

            //app.Use(async (context, next) =>//没有调用 next() 那就是终结点  跟Run一样
            //{
            //    await context.Response.WriteAsync("Hello World Use3  Again Again <br/>");
            //    //await next();
            //});

            //UseWhen可以对HttpContext检测后，增加处理环节;原来的流程还是正常执行的
            //app.UseWhen(context =>
            //{
            //    return context.Request.Query.ContainsKey("Name");
            //},
            //appBuilder =>
            //{
            //    appBuilder.Use(async (context, next) =>
            //    {
            //        await context.Response.WriteAsync("Hello World Use3 Again Again Again <br/>");
            //        //await next();
            //    });
            //});
            #endregion

            #region 自定义中间件 middlewate
            ////根据条件指定中间件 指向终结点，没有Next
            //app.Map("/Test", MapTest);
            //app.Map("/Eleven", a => a.Run(async context =>
            //{
            //    await context.Response.WriteAsync($"This is Advanced Eleven Site");
            //}));
            //app.MapWhen(context =>
            //{
            //    return context.Request.Query.ContainsKey("Name");
            //    //拒绝非chorme浏览器的请求  
            //    //多语言
            //    //把ajax统一处理
            //}, MapTest);
            //以上均为Use的封装，其实是为了熟悉的人方便，或者增加面试的复杂度

            ////UseMiddlerware 类--反射找
            //app.UseMiddleware<FirstMiddleWare>();
            //app.UseMiddleware<SecondMiddleWare>();
            //app.UseMiddleware<ThreeMiddleWare>();

            #endregion

            #region  app config
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot"))
            });

            loggerFactory.AddLog4Net();

            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            #endregion
        }
        private static void MapTest(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"Url is {context.Request.Path.Value}");
            });
        }
    }
}
