using EPM.EFCore.Context;
using EPM.EFCore.Uow;
using EPM.IRepository.Repository;
using EPM.IService.Service;
using EPM.Model.DbModel;
using EPM.Service.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EPM.XUnitTest
{
    public class UserServiceTest
    {
        [Fact]
        public async Task Add_Return()
        {
            // Arrange


            var _dbContext = await new InMemoryDbContextFactory().GetDbContext();
            var repository = new Mock<IUserRepository>();
            var _unitOfWork = new UnitOfWork<AppDbContext>(_dbContext);
            var _tokenService = new Mock<ITokenService>();
            var userService = new UserService(repository.Object,_unitOfWork,_tokenService.Object);

            var user = new User()
            {
                ID = Guid.NewGuid(),
                LoginName = "kevin",
                Password = "123456",
                RoleID=Guid.NewGuid()
            };
            //repository.Setup(p => p.Add(user));

            var users = _dbContext.Users.ToList();
            // Act
            var result = await userService.AddAsync(user);

            // Assert
            Assert.Equal(1, (int)(result.Code));
        }

        //[Fact]
        //public async Task Delete_Return()
        //{
        //    var repository = new Mock<IUserRepository>();
        //    var userService = new UserService(repository.Object);

        //    Guid id = Guid.NewGuid();

        //    repository.Setup(p => p.Delete(id)).Returns(Task.Run(() => { return 1; }));

        //    var result = await userService.Delete(id);

        //    Assert.Equal(1, 1);
        //}

        //[Fact]
        //public async Task DeleteNot_Return()
        //{
        //    var repository = new Mock<IUserRepository>();
        //    var userService = new UserService(repository.Object);

        //    Guid id = Guid.NewGuid();

        //    repository.Setup(p => p.Delete(Guid.NewGuid())).Returns(Task.Run(() => { return 1; }));

        //    var result = await userService.Delete(id);

        //    Assert.Equal(0, 0);
        //}
    }
}
