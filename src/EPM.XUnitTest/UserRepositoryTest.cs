using EPM.EFCore.Context;
using EPM.EFCore.Uow;
using EPM.Model.DbModel;
using EPM.Model.Enum;
using EPM.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace EPM.XUnitTest
{
    [Trait("用户仓储层", "UserRepository")]
    public class UserRepositoryTest
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Add_Success()
        {
            // Arrange
            var _dbContext = await new InMemoryDbContextFactory().GetDbContext();
            var _userRepository = new UserRepository(_dbContext);
            var _unitOfWork = new UnitOfWork<AppDbContext>(_dbContext);
            var user = new User()
            {
                ID = Guid.NewGuid(),
                LoginName = "tom"
            };


            // Act
            _userRepository.Add(user);
            int result = await _unitOfWork.SaveChangesAsync();

            // Assert
            Assert.Equal(1, result);
        }

        /// <summary>
        /// 测试获取所有
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAll_Return()
        {
            // Arrange
            var _dbContext = await new InMemoryDbContextFactory().GetDbContext();
            var _userRepository = new UserRepository(_dbContext);

            // Act
            var users = _userRepository.GetAllListAsync(null);

            // Assert
            Assert.NotNull(users);
        }

        /// <summary>
        /// 测试分页查询
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetPage_Return()
        {
            // Arrange
            var _dbContext = await new InMemoryDbContextFactory().GetDbContext();
            var _userRepository = new UserRepository(_dbContext);

            // Act
            var users = _userRepository.GetPatgeListAsync(new Model.ApiModel.PagingRequest()
            {
                IsPaging=false,
                SortField="LoginName"
            });

            // Assert
            Assert.NotNull(users);
        }

        [Fact]
        public async Task Delete_Return()
        {
            // Arrange
            var _dbContext = await new InMemoryDbContextFactory().GetDbContext();
            var _userRepository = new UserRepository(_dbContext);
            var _unitOfWork = new UnitOfWork<AppDbContext>(_dbContext);

            Guid ID = Guid.Parse("75B29B45-E166-4665-8B09-BA73DE4C5FB0");
            var user = await _dbContext.Users.FirstOrDefaultAsync(p => p.ID == Guid.Parse("D0C28028-68E9-4DC6-A1EA-708207A66521"));
            user.IsDeleted = (int)DeleteFlag.Deleted;

            Expression<Func<User, object>>[] updatedServices =
            {
                   p=>p.IsDeleted
                };

            // Act
            _userRepository.Update(user,updatedServices);

            int result = await _unitOfWork.SaveChangesAsync();

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Put_Return()
        {
            // Arrange
            var _dbContext = await new InMemoryDbContextFactory().GetDbContext();
            var _userRepository = new UserRepository(_dbContext);
            var _unitOfWork = new UnitOfWork<AppDbContext>(_dbContext);

            var user = await _dbContext.Users.FirstOrDefaultAsync(p => p.ID == Guid.Parse("D0C28028-68E9-4DC6-A1EA-708207A66521"));
            user.LoginName = "tom";

            Expression<Func<User, object>>[] updatedServices =
            {
               p=>p.LoginName
            };

            // Act
            _userRepository.Update(user, updatedServices);

            int result = await _unitOfWork.SaveChangesAsync();

            // Assert
            Assert.Equal(1, result);
        }
    }
}
