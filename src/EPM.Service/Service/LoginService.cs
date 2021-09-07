using EPM.IService.Service;
using EPM.Model.ApiModel;
using EPM.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Service.Service
{
    public class LoginService : ILoginService
    {
        private readonly ITokenService _tokenService;

        public LoginService(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }


        /// <summary>
        /// 用户退出当前登录
        /// </summary>
        /// <returns></returns>
        public async Task<LoginStatus> LoginOut()
        {
            // 从token中获取当前用户
            LoginInfo loginInfo = await _tokenService.GetLoginInfoByToken();
            // 清除Redis中当前用户的token信息

            return LoginStatus.LoginOut;
        }
    }
}
