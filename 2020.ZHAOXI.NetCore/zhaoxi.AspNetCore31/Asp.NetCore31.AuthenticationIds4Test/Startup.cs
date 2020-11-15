using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Asp.NetCore31.AuthenticationIds4Test.DataInit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Asp.NetCore31.AuthenticationIds4Test
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
            //services.AddControllers();
            // config for Identity Server4 UI
            services.AddControllersWithViews();

            //#region 客户端
            //services.AddIdentityServer()//怎么处理
            //  .AddDeveloperSigningCredential()//默认的开发者证书--临时证书--生产环境为了保证token不失效，证书是不变的
            //  .AddInMemoryClients(ClientInitConfig.GetClients())//InMemory 内存模式
            //  .AddInMemoryApiResources(ClientInitConfig.GetApiResources())//能访问啥资源
            //  .AddTestUsers(PasswordInitConfig.GetUsers());//添加用户
            //#endregion

            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()        //This is for dev only scenarios when you don’t have a certificate to use.
                    .AddInMemoryApiScopes(ClientInitConfig.ApiScopes)
                     .AddInMemoryApiScopes(ClientInitConfig.ApiScopes)
                    .AddInMemoryClients(ClientInitConfig.Clients);

            #region 密码模式
            //services.AddIdentityServer()
            //    .AddDeveloperSigningCredential()//默认的开发者证书 
            //   .AddInMemoryApiResources(PasswordInitConfig.GetApiResources())//API访问授权资源
            //   .AddInMemoryClients(PasswordInitConfig.GetClients())  //客户端
            //   .AddTestUsers(PasswordInitConfig.GetUsers());//添加用户
            #endregion



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            //访问wwwroot目录静态文件
            app.UseStaticFiles(
                new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot"))
                });


            #region add UseIdentityServer
            app.UseIdentityServer();
            #endregion

          
            app.UseRouting();

            app.UseAuthorization();

            //使用Mvc中间件
          //  app.UseMvcWithDefaultRoute();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                // config for Identity Server4 UI
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
