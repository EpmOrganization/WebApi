using EPM.EFCore.Context;
using EPM.Model.DbModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPM.XUnitTest
{
    /// <summary>
    /// 内存数据库
    /// </summary>
    public class InMemoryDbContextFactory
    {
        public async Task<AppDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                            .UseInMemoryDatabase(databaseName: "InMemoryArticleDatabase")
                            .Options;
            var dbContext = new AppDbContext(options);

            List<User> list = new List<User>()
                {
                    new User()
                    {
                        ID = Guid.Parse("75B29B45-E166-4665-8B09-BA73DE4C5FB0"),
                        LoginName = "admin",
                        Name="管理员",
                        Password = "1234",
                        CreateTime=DateTime.Parse("2021-08-26 14:14:19.000000")
                    },
                    new User()
                    {
                        ID = Guid.Parse("D0C28028-68E9-4DC6-A1EA-708207A66521"),
                        LoginName = "test",
                          Name="测试",
                        Password = "123456",
                         CreateTime=DateTime.Parse("2021-08-25 12:34:15.000000")
                    }
                };

            await dbContext.Users.AddRangeAsync(list);
            await dbContext.SaveChangesAsync();
            return dbContext;
        }
    }
}
