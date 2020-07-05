using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Center.Interface;
using log4net.Repository.Hierarchy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;
using Microsoft.Extensions.DependencyInjection;

namespace Center.Web.Controllers
{
    public class SecondController : Controller
    {
        private readonly ILogger<SecondController> _logger;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ITestServiceA _iTestServiceA;
        private readonly ITestServiceB _iTestServiceB;
        private readonly ITestServiceC _iTestServiceC;
        private readonly ITestServiceD _iTestServiceD;
        private readonly ITestServiceE _iTestServiceE;
        private readonly IServiceProvider _iServiceProvider;

        public SecondController(ILogger<SecondController> logger,
            ILoggerFactory loggerFactory,
            ITestServiceA testServiceA,
            ITestServiceB testServiceB,
            ITestServiceC testServiceC,
            ITestServiceD testServiceD,
            ITestServiceE testServiceE,
            IServiceProvider iServiceProvider
            )
        {
            _logger = logger;
            this._loggerFactory = loggerFactory;
            this._iTestServiceA = testServiceA;
            this._iTestServiceB = testServiceB;
            this._iTestServiceC = testServiceC;
            this._iTestServiceD = testServiceD;
            this._iTestServiceE = testServiceE;
            this._iServiceProvider = iServiceProvider;
        }

        /// <summary>
        /// 纯属测试 毫无意义
        /// </summary>
        private static ITestServiceC _iTestServiceCStatic = null;
        private static ITestServiceB _iTestServiceBStatic = null;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            //ITestServiceA testServiceA = new TestServiceA();
            this._logger.LogWarning("This is SecondController Index");

            //cc True
            //C & C False
            //B & B：True
            //ServiceProvoder 也是作用域单例的，同一个ServiceProvider产生的C都是一样的
            //一个请求只会产生一个ServiceProvider
            var c = this._iServiceProvider.GetService<ITestServiceC>(); //实例化服务
            
            //test c
            Console.WriteLine($"cc {object.ReferenceEquals(this._iTestServiceC, c)}");//T/F

            //test static c
            if (_iTestServiceCStatic == null)
            {
                _iTestServiceCStatic = _iTestServiceC;
            }
            else
            {
                Console.WriteLine($"C&C {object.ReferenceEquals(this._iTestServiceC, _iTestServiceCStatic)}");//两次不同的请求  //T/F
            }

            //test static b
            if (_iTestServiceBStatic == null)
            {
                _iTestServiceBStatic = _iTestServiceB;
            }
            else
            {
                Console.WriteLine($"B&B：{object.ReferenceEquals(this._iTestServiceB, _iTestServiceBStatic)}");//两次不同的请求  //T/F
            }

            this._iTestServiceA.Show();
            this._iTestServiceB.Show();
            this._iTestServiceC.Show();
            this._iTestServiceD.Show();
            this._iTestServiceE.Show();

            return View();
        }
    }
}