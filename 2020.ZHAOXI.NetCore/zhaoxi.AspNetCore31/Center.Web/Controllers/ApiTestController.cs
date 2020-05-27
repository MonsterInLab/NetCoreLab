using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Center.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Center.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiTestController : ControllerBase
    {
        [HttpGet]
        [Route("All")]
        public CurrentUser All()
        {
            CurrentUser currentUser = new CurrentUser()
            {
                Id = 123,
                Name = "Eleven",
                Account = "Administrator",
                Email = "57265177",
                Password = "123456",
                LoginTime = DateTime.Now
            };
            return currentUser;
        }

        [HttpGet]
        [Route("Get")]
        public CurrentUser Get()
        {
            CurrentUser currentUser = new CurrentUser()
            {
                Id = 123,
                Name = "Eleven",
                Account = "Administrator",
                Email = "57265177",
                Password = "123456",
                LoginTime = DateTime.Now
            };
            return currentUser;
        }

        [HttpGet]
        public CurrentUser GetUser()
        {
            CurrentUser currentUser = new CurrentUser()
            {
                Id = 123,
                Name = "Eleven",
                Account = "Administrator",
                Email = "57265177",
                Password = "123456",
                LoginTime = DateTime.Now
            };
            return currentUser;
        }

        // GET: api/ApiTest/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ApiTest
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/ApiTest/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
