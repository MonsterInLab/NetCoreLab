using Center.EntityFrameworkCore31.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Center.Interface
{
    public  interface IUserService : IBaseService
    {
        //void Query();
        //void Update();
        //void Delete();
        //void Add();

        void UpdateLastLogin(User user);
    }
}
