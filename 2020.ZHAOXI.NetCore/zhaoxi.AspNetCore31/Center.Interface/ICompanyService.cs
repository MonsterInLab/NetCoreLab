using Data.EFCCore31.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Center.Interface
{
    public interface ICompanyService
    {
        PageResult<Company> GetCompanyList(int pageSize, int pageIndex);

        Company GetCompany(int id);

    }
}
