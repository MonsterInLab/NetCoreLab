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
using Center.Interface;
using Center.Service;
using Autofac;
using Microsoft.AspNetCore.Authentication.Cookies;


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
          //  services.AddControllersWithViews();
            services.AddControllersWithViews(
                options =>
                {
                    options.Filters.Add<CustomExceptionFilterAttribute>();//ȫ��ע���쳣����
                    options.Filters.Add<CustomGlobalFilterAttribute>();//ȫ��ע���쳣����
                }).AddRazorRuntimeCompilation();//�޸�cshtml�����Զ�����

            // ��Ȩ  ��Ȩ
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => 
                {
                    options.LoginPath = new PathString("/Fourth/Login");
                    options.AccessDeniedPath = new PathString("/Home/Privacy");
                });//��cookie�ķ�ʽ��֤��˳���ʼ����¼��ַ

            services.AddScoped(typeof(CustomExceptionFilterAttribute)); //�������� 

            //��������ʵ������ServiceProvider����������Ǹ�������ģ��������߳�û��ϵ
            services.AddTransient<ITestServiceA, TestServiceA>(); //˲ʱ
            services.AddSingleton<ITestServiceB, TestServiceB>(); //����
            services.AddScoped<ITestServiceC, TestServiceC>();    //������
            services.AddTransient<ITestServiceD, TestServiceD>();
          //  services.AddTransient<ITestServiceE, TestServiceE>();
        }

        
        //Autofac ����ע�����
        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
          //  containerBuilder.RegisterType<TestServiceE>().As<ITestServiceE>().SingleInstance();
            containerBuilder.RegisterModule<CustomAutofacModule>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILoggerFactory loggerFactory)
        {

            //app.Run(context =>
            //{
            //   return context.Response.WriteAsync("This is hello word");
            //}); 

            #region Use�м�� 
            //�ս�ʱ
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

            //app.Use(async (context, next) =>//û�е��� next() �Ǿ����ս��  ��Runһ��
            //{
            //    await context.Response.WriteAsync("Hello World Use3  Again Again <br/>");
            //    //await next();
            //});

            //UseWhen���Զ�HttpContext�������Ӵ�����;ԭ�������̻�������ִ�е�
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

            #region �Զ����м�� middleware
            ////��������ָ���м�� ָ���ս�㣬û��Next
            //app.Map("/Test", MapTest);
            //app.Map("/Eleven", a => a.Run(async context =>
            //{
            //    await context.Response.WriteAsync($"This is Advanced Eleven Site");
            //}));
            //app.MapWhen(context =>
            //{
            //    return context.Request.Query.ContainsKey("Name");
            //    //�ܾ���chorme�����������  
            //    //������
            //    //��ajaxͳһ����
            //}, MapTest);
            //���Ͼ�ΪUse�ķ�װ����ʵ��Ϊ����Ϥ���˷��㣬�����������Եĸ��Ӷ�

            ////UseMiddlerware ��--������
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

            app.UseAuthentication(); //��Ȩ�������û�е�½����¼����˭����ֵ��User

            app.UseAuthorization(); //��Ȩ�����Ȩ��

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
