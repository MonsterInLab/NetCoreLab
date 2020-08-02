using Data.EFCCore31.Models;
using Data.EFCCore31.SqlLog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Data.EFCCore31
{
    public class AppDbContext : DbContext
    {

        private IConfiguration _configuration = null;
        public ILoggerFactory _loggerFactory = null;

        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public AppDbContext(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            this._configuration = configuration;
            this._loggerFactory = loggerFactory;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //{
            //    var builder = new ConfigurationBuilder()
            //                  .SetBasePath(Directory.GetCurrentDirectory())
            //                  .AddJsonFile("appsettings.json");
            //    var configuration = builder.Build();
            //    var conn = configuration.GetConnectionString("JDDbConnection");

            //    var connetionString = "Server=.;Database=DB.zhaoxi.demo2;User id=admin;password=123456;Trusted_Connection=True;";
            //    optionsBuilder.UseSqlServer(connetionString);
            //}

            // IOC
            optionsBuilder.UseSqlServer(this._configuration.GetConnectionString("JDDbConnection"));

            //optionsBuilder.UseLoggerFactory(this._loggerFactory);
            
            optionsBuilder.UseLoggerFactory(new CustomEFLoggerFactory());


        }

        #region DbSet
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public DbSet<SysUser> SysUser { get; set; }
        public virtual DbSet<SysLog> SysLogs { get; set; }
        public virtual DbSet<SysMenu> SysMenus { get; set; }
        public virtual DbSet<SysRole> SysRoles { get; set; }
        public virtual DbSet<SysRoleMenuMapping> SysRoleMenuMappings { get; set; }
        public virtual DbSet<SysUser> SysUsers { get; set; }
        public virtual DbSet<SysUserMenuMapping> SysUserMenuMappings { get; set; }
        public virtual DbSet<SysUserRoleMapping> SysUserRoleMappings { get; set; }
        public virtual DbSet<User> Users { get; set; }
        #endregion

    }
}
