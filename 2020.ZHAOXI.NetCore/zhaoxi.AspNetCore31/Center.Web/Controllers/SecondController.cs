using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Center.Interface;
using log4net.Repository.Hierarchy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;

namespace Center.Web.Controllers
{
    public class SecondController : Controller
    {
        private readonly ILogger<SecondController> _logger;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ITestServiceA _iTestserviceA;

        public SecondController(ILogger<SecondController> logger,
            ILoggerFactory loggerFactory,
            ITestServiceA testServiceA)
        {
            _logger = logger;
            this._loggerFactory = loggerFactory;
            this._iTestserviceA = testServiceA;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}