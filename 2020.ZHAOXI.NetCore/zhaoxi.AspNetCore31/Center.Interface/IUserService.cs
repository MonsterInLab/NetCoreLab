using Data.EFCCore31.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Center.Interface
{
    public  interface IUserService : IBaseService
    {
        User GetUserByID(int id);
        PageResult<User> GetUserList(int pageSize, int pageIndex);

        void Update(User user);
        void Delete(int id);
        bool Add(User user);

        void UpdateLastLogin(User user);
    }
}
