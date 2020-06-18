using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Center.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
          //  CreateHostBuilder(args).Build().Run();
            
            //CreateHostBuilder(args).Build().Run();//启动个kestrel
            var hostBuilder = CreateHostBuilder(args);//完成配置--完成Kestrel
            //hostBuilder.ConfigureWebHost()
            var host = hostBuilder.Build();//
            //IHostedService
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureLogging(loggingBuilder =>
            {
                loggingBuilder.AddLog4Net();//需要配置文件
                DefaultServiceProviderFactory
            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    
                    #region Kestrl test
                    //webBuilder.UseKestrel(o =>
                    //{
                    //    o.Listen(IPAddress.Loopback, 12344);
                    //})
                    //.Configure(app=>app.Run(async context => await context.Response.WriteAsync("Hello word 123")))
                    //.UseIIS()
                    //.UseIISIntegration();
                    #endregion

                    webBuilder.UseStartup<Startup>();
                });
    }
}
