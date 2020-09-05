using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Center.Interface;
using Center.Service;
using Center.WebAPI.Utility;
using Data.EFCCore31;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Center.WebAPI
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

            #region JWT鉴权授权
            //1.Nuget引入程序包：Microsoft.AspNetCore.Authentication.JwtBearer 
            //services.AddAuthentication();//禁用  
            var validAudience = this.Configuration["audience"];
            var validIssuer = this.Configuration["issuer"];
            var securityKey = this.Configuration["SecurityKey"];
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)  //默认授权机制名称；                                      
                     .AddJwtBearer(options =>
                     {
                         options.TokenValidationParameters = new TokenValidationParameters
                         {
                             ValidateIssuer = true,//是否验证Issuer
                             ValidateAudience = true,//是否验证Audience
                             ValidateLifetime = true,//是否验证失效时间
                             ValidateIssuerSigningKey = true,//是否验证SecurityKey
                             ValidAudience = validAudience,//Audience
                             ValidIssuer = validIssuer,//Issuer，这两项和前面签发jwt的设置一致  表示谁签发的Token
                             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey))//拿到SecurityKey
                             //AudienceValidator = (m, n, z) =>
                             //{ 
                             //    return m != null && m.FirstOrDefault().Equals(this.Configuration["audience"]);
                             //},//自定义校验规则，可以新登录后将之前的无效 
                         };
                     });
            #endregion

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();

            services.AddControllers();

            //全局注册异常处理,允许跨域
            //services.AddControllers(
            //    options =>
            //    {
            //        options.Filters.Add<CustomGlobalFilterAttribute>();//全局注册异常处理,允许跨域
            //    });


            services.AddTransient<ICompanyService, CompanyService>();

            #region  DbContext IOC
            //读取连接字符串
            services.AddScoped<DbContext, AppDbContext>();

            // User Service
            services.AddScoped<IUserService, UserService>();

            //services.AddEntityFrameworkSqlServer()
            //    .AddDbContext<AppDbContext>(option =>
            //    {
            //        option.UseSqlServer(Configuration.GetConnectionString("JDDbConnection")); //读取连接字符串
            //    });

            //services.AddDbContext<AppDbContext>(options =>
            //{
            //    options.UseSqlServer(Configuration.GetConnectionString("JDDbConnection"));
            //});
            #endregion



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddLog4Net();

            #region Use Swagger
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            #endregion

            app.UseRouting();

            app.UseAuthentication(); //鉴权，检测有没有登陆，登录的是谁，赋值给User


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
