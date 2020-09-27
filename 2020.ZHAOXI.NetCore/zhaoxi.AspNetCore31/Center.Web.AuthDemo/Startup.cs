using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Center.Web.AuthDemo.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace Center.Web.AuthDemo
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


            #region Filter方式
            //services.AddAuthentication()
            //.AddCookie();
            #endregion


            #region Authorize
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //})
            //.AddCookie();
            #endregion

            #region 最基础认证--自定义Handler
            //services.AddAuthenticationCore();
            //services.AddAuthentication().AddCookie();
            //services.AddAuthenticationCore(options => 
            //     options.AddScheme<CustomAuthenticationHandler>("CustomScheme.ZX", "DemoScheme.ZX")
            //);
            #endregion

            #region 基于Cookie
            //services.AddScoped<ITicketStore, MemoryCacheTicketStore>();
            //services.AddMemoryCache();
            ////services.AddDistributedRedisCache(options =>
            ////{
            ////    options.Configuration = "127.0.0.1:6379";
            ////    options.InstanceName = "RedisDistributedSession";
            ////});
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;//不能少
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "Cookie/Login";
            })
            .AddCookie(options =>
            {
                ////信息存在服务端--把key写入cookie--类似session
                //options.SessionStore = services.BuildServiceProvider().GetService<ITicketStore>();
                //options.Events = new CookieAuthenticationEvents()
                //{
                //    OnSignedIn = new Func<CookieSignedInContext, Task>(
                //        async context =>
                //        {
                //            Console.WriteLine($"{context.Request.Path} is OnSignedIn");
                //            await Task.CompletedTask;
                //        }),
                //    OnSigningIn = async context =>
                //    {
                //        Console.WriteLine($"{context.Request.Path} is OnSigningIn");
                //        await Task.CompletedTask;
                //    },
                //    OnSigningOut = async context =>
                //    {
                //        Console.WriteLine($"{context.Request.Path} is OnSigningOut");
                //        await Task.CompletedTask;
                //    }
                //};//扩展事件
            });

            //new AuthenticationBuilder().AddCookie()
            #endregion


            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
          //  app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot"))
            });

            app.UseRouting();

            //Authentication 鉴权  是否登录
            app.UseAuthentication();

            app.UseAuthorization(); //授权  -- 角色权限

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
