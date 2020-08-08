using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Center.Interface;
using Data.EFCCore31.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Center.Web.Areas.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _iCompanyService;

        public CompanyController(ICompanyService companyService)
        {
            this._iCompanyService = companyService;
        }


        [HttpGet]
        public ActionResult <IEnumerable<Company>> GetCompanies()
        {
            var pageIndex = 1;
            var pageSize = 10;
            var data = this._iCompanyService.GetCompanyList(pageSize, pageIndex);
            return Ok(data.DataList);
        }

        [HttpGet("{id}")]
        public ActionResult<Company> GetCompany(int id)
        {
            var company = this._iCompanyService.GetCompany(id);
            return company;
        }

    }
}
