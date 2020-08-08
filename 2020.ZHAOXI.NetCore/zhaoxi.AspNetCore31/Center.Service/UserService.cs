using Center.Interface;
using Data.EFCCore31.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Center.Service
{
    public class UserService : BaseService, IUserService
    {
        public UserService(DbContext context) : base(context)
        {
        }

        public void UpdateLastLogin(User user)
        {
            User userDB = base.Find<User>(user.Id);
            userDB.LastLoginTime = DateTime.Now;
            this.Commit();
        }
        public bool Add(User user)
        {
            var u = base.Insert<User>(user);
            return u.Id > 0;
        }

        public void Delete(int id)
        {
            base.Delete<User>(id);
        }


        public void Update(User user)
        {
            base.Update<User>(user);
        }

        #region Query
        public User GetUserByID(int id)
        {
            User userDB = base.Find<User>(id);
         //   var userList = this._iUserService.QueryPage<User, int>(u => u.Id > 0, 5, 1, u => u.Id);
            return userDB;
        }

        public PageResult<User> GetUserList(int pageSize,int pageIndex)
        {
            var userList = base.QueryPage<User, int>(u => u.Id > 0, pageSize, pageIndex, u => u.Id);

            return userList;
        }


        #endregion
    }
}
