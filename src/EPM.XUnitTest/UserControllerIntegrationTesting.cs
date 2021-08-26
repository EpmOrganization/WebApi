using EPM.Model.DbModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace EPM.XUnitTest
{
    public class UserControllerIntegrationTesting : ApiControllerTestBase
    {
        /// <summary>
        /// HttpClient对象
        /// </summary>
        public HttpClient Client { get; }

        /// <summary>
        /// 用来输出返回值
        /// </summary>
        public ITestOutputHelper Output { get; }

        public UserControllerIntegrationTesting(ITestOutputHelper outputHelper)
        {
            // 调用父类
            Client = GetClient();
            Output = outputHelper;
        }

        [Fact]
        public async Task Add_Shouldbe_Success()
        {
            // 1、Arrange
            var user = new User()
            {
                LoginName = "jerry",
                Password = "123456"
            };

            var str = JsonConvert.SerializeObject(user);
            HttpContent content = new StringContent(str);

            // 2、Act
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await Client.PostAsync("https://localhost/api/users", content);
            string responseBody = await response.Content.ReadAsStringAsync();
            Output.WriteLine(responseBody);

            int result = Convert.ToInt32(responseBody);
            // 3、Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Add_Shouldbe_Fail()
        {
            // 1、Arrange
            var user = new User()
            {
                LoginName = "jerry",
                Password = "123456"
            };

            var str = JsonConvert.SerializeObject(user);
            HttpContent content = new StringContent(str);

            // 2、Act
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await Client.PostAsync("https://localhost/api/users", content);
            string responseBody = await response.Content.ReadAsStringAsync();
            Output.WriteLine(responseBody);

            int result = Convert.ToInt32(responseBody);
            // 3、Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task Get_Shouldbe_Success()
        {
            HttpResponseMessage response = await Client.GetAsync("https://localhost/api/users");
            string responseBody = await response.Content.ReadAsStringAsync();
            Output.WriteLine(responseBody);

            var result = JsonConvert.DeserializeObject<List<User>>(responseBody);
            // 3、Assert
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task Delete_Shouldbe_Success()
        {
            HttpResponseMessage response = await Client.DeleteAsync("https://localhost/api/users?id=3DB05C6B-C569-499F-80AC-880F89A4F3F4");
            string responseBody = await response.Content.ReadAsStringAsync();
            Output.WriteLine(responseBody);

            int result = Convert.ToInt32(responseBody);
            // 3、Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Put_Shouldbe_Success()
        {
            var user = new User()
            {
                ID = Guid.Parse("677D9EB9-4428-429E-BACE-09255AB9196A"),
                LoginName = "kevin",
                Password = "123456"
            };

            var str = JsonConvert.SerializeObject(user);
            HttpContent content = new StringContent(str);

            // 2、Act
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await Client.PutAsync("https://localhost/api/users", content);
            string responseBody = await response.Content.ReadAsStringAsync();
            Output.WriteLine(responseBody);

            int result = Convert.ToInt32(responseBody);
            // 3、Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Put_Shouldbe_Fail()
        {
            var user = new User()
            {
                ID = Guid.NewGuid(),
                LoginName = "test",
                Password = "123456"
            };

            var str = JsonConvert.SerializeObject(user);
            HttpContent content = new StringContent(str);

            // 2、Act
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await Client.PutAsync("https://localhost/api/users", content);
            string responseBody = await response.Content.ReadAsStringAsync();
            Output.WriteLine(responseBody);

            int result = Convert.ToInt32(responseBody);
            // 3、Assert
            Assert.Equal(0, result);
        }
    }
}
