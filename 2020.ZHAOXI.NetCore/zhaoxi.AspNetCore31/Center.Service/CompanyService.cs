using Center.Interface;
using Data.EFCCore31.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Center.Service
{
    public class CompanyService : BaseService,ICompanyService
    {

        public CompanyService(DbContext context) : base(context)
        {
        }


        public Company GetCompany(int id)
        {
           var item = base.Find<Company>(id);
            return item;
        }

        public PageResult<Company> GetCompanyList(int pageSize, int pageIndex)
        {
            var userList = base.QueryPage<Company, int>(u => u.Id > 0, pageSize, pageIndex, u => u.Id);

            return userList;
        }
    }
}
