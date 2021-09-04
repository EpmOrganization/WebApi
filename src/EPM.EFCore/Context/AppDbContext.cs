using EPM.Model.DbModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPM.EFCore.Context
{
    /// <summary>
    /// 数据上下文类，继承自DbContext
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// 通过构造函数给父类传参
        /// </summary>
        /// <param name="options"></param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        #region DbSet属性

        /// <summary>
        /// 用户表
        /// </summary>
        public DbSet<User> Users { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<RoleMenu> RoleMenus { get; set; }

        public DbSet<WorkItem> WorkItems { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 填充种子数据

            Guid guid = Guid.NewGuid();
            Guid deptGuid = Guid.NewGuid();
            // 角色ID
            Guid roleGuid = Guid.NewGuid();

            // 填充角色数据
            modelBuilder.Entity<Role>().HasData(new Role()
            {
                ClusterID = 1,
                ID = roleGuid,
                CreateUser = "系统管理员",
                UpdateUser = "系统管理员",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                Name = "系统管理员"
            });

            // 填充用户数据
            modelBuilder.Entity<User>().HasData(new User()
            {
                ClusterID = 1,
                ID = guid,
                CreateUser = "系统管理员",
                UpdateUser = "系统管理员",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                LoginName = "admin",
                Password = "e10adc3949ba59abbe56e057f20f883e",
                Name = "系统管理员",
                DepartmentID = deptGuid,
                RoleID = roleGuid,
            });
            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}
