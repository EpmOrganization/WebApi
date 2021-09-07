using EPM.IService.Service;
using EPM.Model.ApiModel;
using EPM.Model.Enum;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EPM.WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenService"></param>
        /// <param name="loginService"></param>
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("logOut")]
        public async Task<ActionResult<ApiResponse>> LogOut()
        {
            ApiResponse result = new ApiResponse();
            LoginStatus loginStatus = await _loginService.LoginOut();
            return ApiResponse.Success();
        }
    }
}
