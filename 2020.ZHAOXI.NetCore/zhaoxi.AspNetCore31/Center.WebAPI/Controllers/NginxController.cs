using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Center.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NginxController : ControllerBase
    {
        private readonly ILogger<NginxController> _logger;

        public NginxController(ILogger<NginxController> logger)
        {
            this._logger = logger;
            this._logger.LogInformation("NginxController 被实例化");
        }

        private static int count = 0;

        [HttpGet]
        [Route("GetString")]
        public string GetString()
        {
            count++;
            return Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                Port = "This is 001 服务器",
                VisitedCount = count
            });

        }

    }
}
