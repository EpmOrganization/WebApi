using EPM.IService.Service;
using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.WebApi.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EPM.XUnitTest
{
    public class UserControllerTest
    {
        [Fact]
        public async Task Add_Return()
        {
            var userService = new Mock<IUserService>();

            UserController usersController = new UserController(userService.Object);

            var user = new User()
            {
                ID = Guid.NewGuid(),
                LoginName = "kevin",
                Password = "123456"
            };
            userService.Setup(p => p.AddAsync(user)).Returns(Task.Run(() => 
            { 
                return new ValidateResult() 
                {
                  Code=1,
                  Msg="操作成功"
                }; 
            }));

            var result = await usersController.Post(user);

            Assert.Equal(1, 1);
        }


        [Fact]
        public async Task Delete_Return()
        {
            var userService = new Mock<IUserService>();

            UserController usersController = new UserController(userService.Object);

            Guid id = Guid.NewGuid();

            userService.Setup(p => p.DeleteAsync(id)).Returns(Task.Run(() => 
            {
                return new ValidateResult()
                {
                    Code = 1,
                    Msg = "操作成功"
                };
            }));

            var result = await usersController.Delete(id);

            Assert.Equal(1, 1);
        }

        [Fact]
        public async Task DeleteNot_Return()
        {
            var userService = new Mock<IUserService>();

            UserController usersController = new UserController(userService.Object);

            Guid id = Guid.NewGuid();

            userService.Setup(p => p.DeleteAsync(Guid.NewGuid())).Returns(Task.Run(() =>
            {
                return new ValidateResult()
                {
                    Code = 1,
                    Msg = "操作成功"
                };
            }));

            var result = await usersController.Delete(id);

            Assert.Equal(0, 0);
        }
    }
}
