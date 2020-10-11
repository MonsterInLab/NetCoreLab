using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
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


            #region Filter��ʽ
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

            #region �������֤--�Զ���Handler
            //services.AddAuthenticationCore();
            //services.AddAuthentication().AddCookie();
            //services.AddAuthenticationCore(options => 
            //     options.AddScheme<CustomAuthenticationHandler>("CustomScheme.ZX", "DemoScheme.ZX")
            //);
            #endregion

            #region ����Cookie��Ȩ
            //services.AddScoped<ITicketStore, MemoryCacheTicketStore>();
            //services.AddMemoryCache();
            ////services.AddDistributedRedisCache(options =>
            ////{
            ////    options.Configuration = "127.0.0.1:6379";
            ////    options.InstanceName = "RedisDistributedSession";
            ////});

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;//������
            //    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = "Cookie/Login";
            //})
            //.AddCookie(options =>
            //{
            ////��Ϣ���ڷ����--��keyд��cookie--����session
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
            //};//��չ�¼�
            // });

            //new AuthenticationBuilder().AddCookie()
            #endregion

            #region ����Cookies��Ȩ---��ɫ��Ȩ
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    //������,signin signout Authenticate���ǻ���Scheme
            //    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //})
            //.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            //{
            //    options.LoginPath = "/Authorization/Index";
            //    options.AccessDeniedPath = "/Authorization/Index";
            //});
            ////.AddCookie("CustomScheme", options =>
            ////{
            ////    options.LoginPath = "/Authorization/Index";
            ////    options.AccessDeniedPath = "/Authorization/Index";
            ////});

            #endregion

            #region ���ڲ�����Ȩ
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;//������
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/Authorization/Index";
                options.AccessDeniedPath = "/Authorization/Index";
            });

            ////����һ�����õ�policy
            //var qqEmailPolicy = new AuthorizationPolicyBuilder().AddRequirements(new QQEmailRequirement()).Build();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy",
                    policyBuilder => policyBuilder
                    .RequireRole("Admin")//Claim��Role��Admin
                    .RequireUserName("Eleven")//Claim��Name��Eleven
                    .RequireClaim(ClaimTypes.Email)//������ĳ��Cliam
                    //.Combine(qqEmailPolicy)
                    );//����

                options.AddPolicy("UserPolicy",
                    policyBuilder => policyBuilder.RequireAssertion(context =>
                    context.User.HasClaim(c => c.Type == ClaimTypes.Role)
                    && context.User.Claims.First(c => c.Type.Equals(ClaimTypes.Role)).Value == "Admin")
               //.Combine(qqEmailPolicy)
               );//�Զ���
                //policy����  û��Requirements


                //options.AddPolicy("QQEmail", policyBuilder => policyBuilder.Requirements.Add(new QQEmailRequirement()));
               // options.AddPolicy("DoubleEmail", policyBuilder => policyBuilder.Requirements.Add(new DoubleEmailRequirement()));
            });
           // services.AddSingleton<IAuthorizationHandler, ZhaoxiMailHandler>();
           // services.AddSingleton<IAuthorizationHandler, QQMailHandler>();


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

            //Authentication ��Ȩ  �Ƿ��¼
            app.UseAuthentication();

            app.UseAuthorization(); //��Ȩ  -- ��ɫȨ��

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
