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

            //#region �ͻ���
            //services.AddIdentityServer()//��ô����
            //  .AddDeveloperSigningCredential()//Ĭ�ϵĿ�����֤��--��ʱ֤��--��������Ϊ�˱�֤token��ʧЧ��֤���ǲ����
            //  .AddInMemoryClients(ClientInitConfig.GetClients())//InMemory �ڴ�ģʽ
            //  .AddInMemoryApiResources(ClientInitConfig.GetApiResources())//�ܷ���ɶ��Դ
            //  .AddTestUsers(PasswordInitConfig.GetUsers());//����û�
            //#endregion

            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()        //This is for dev only scenarios when you don��t have a certificate to use.
                    .AddInMemoryApiScopes(ClientInitConfig.ApiScopes)
                     .AddInMemoryApiScopes(ClientInitConfig.ApiScopes)
                    .AddInMemoryClients(ClientInitConfig.Clients);

            #region ����ģʽ
            //services.AddIdentityServer()
            //    .AddDeveloperSigningCredential()//Ĭ�ϵĿ�����֤�� 
            //   .AddInMemoryApiResources(PasswordInitConfig.GetApiResources())//API������Ȩ��Դ
            //   .AddInMemoryClients(PasswordInitConfig.GetClients())  //�ͻ���
            //   .AddTestUsers(PasswordInitConfig.GetUsers());//����û�
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

            //����wwwrootĿ¼��̬�ļ�
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

            //ʹ��Mvc�м��
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
