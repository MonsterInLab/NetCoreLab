using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Center.Interface;
using Data.EFCCore31.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Center.Web.Areas.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _iCompanyService;
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ILogger<CompanyController> logger, 
            ICompanyService companyService)
        {
            this._iCompanyService = companyService;
            this._logger = logger;
        }


        [HttpGet]
        [Route("GetCompanies")]
        public ActionResult <IEnumerable<Company>> GetCompanies(int pageIndex,int pageSize)
        {
            this._logger.LogWarning("This is api  CompanyController.GetCompanies");

            var data = this._iCompanyService.GetCompanyList(pageSize, pageIndex);
           // return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(data.DataList));
            return Ok(data);
        }

        [HttpGet("{id}")]
        public ActionResult<Company> GetCompany(int id)
        {
            var company = this._iCompanyService.GetCompany(id);
            return company;
        }

    }
}
