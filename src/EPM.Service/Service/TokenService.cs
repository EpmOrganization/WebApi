
using EPM.Framework.Security;
using EPM.IRepository.Repository;
using EPM.IService.Service;
using EPM.Model.ApiModel;
using EPM.Model.ConfigModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace EPM.Service.Service
{
    public class TokenService : ITokenService
    {
        private readonly JwtConfig _jwtConfig;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        //private readonly IDataAuthorityRepository _dataAuthorityRepository;
        //private readonly IConfigurationHelper _configurationHelper;

        public TokenService(IOptions<JwtConfig> jwtConfig, IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository
           //IDataAuthorityRepository dataAuthorityRepository,
           /* IConfigurationHelper configurationHelper*/)
        {
            _jwtConfig = jwtConfig.Value;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            //_dataAuthorityRepository = dataAuthorityRepository;
            //_configurationHelper = configurationHelper;
        }

        public async Task<LoginInfo> GetLoginInfoByToken()
        {
            LoginInfo loginInfo = new LoginInfo();
            var request = _httpContextAccessor.HttpContext.Request;
            //string authorization = request.Headers["Authorization"].ToString();
            /*
             {"Data":"AxjrXkbU9vRm93nyn5UjF09uc413swJnUSMa2E4OJJEKjBJwwhDxHdmGOP9ty6PU8cdLURfIEXL5VTYK4yufrheomiTI17W7pqDG6a+zGI3zyvwjN+pVxMJluXO1ErPMvOOqn4YtSBvDastAUymEUsWAT/ghkaWsmRUYv1QkLu4zTanjtRtwox3OD1b3ZO5i3obwOxadQvK+hxn+ycDBNpuTygHciDdZpByFC/Gb3LVF0RPgTCE6+kmU/z0eYwiKJUl4dhOu+l2fMuTeEJwdqpSJpq7+iP8rVej9mM5NQa1n6BTHsp42rDNdSo7/K4Fz","Timestamp":1630653484,"Sign":"bd36e2ae3f85dcf32a5084c40fb5a92a"}
             
             */

            string authorization = "{\"SData\":\"AxjrXkbU9vRm93nyn5UjF09uc413swJnUSMa2E4OJJEKjBJwwhDxHdmGOP9ty6PU8cdLURfIEXL5VTYK4yufrheomiTI17W7pqDG6a + zGI3zyvwjN + pVxMJluXO1ErPMvOOqn4YtSBvDastAUymEUsWAT / ghkaWsmRUYv1QkLu4zTanjtRtwox3OD1b3ZO5i3obwOxadQvK + hxn + ycDBNpuTygHciDdZpByFC / Gb3LVF0RPgTCE6 + kmU / z0eYwiKJUl4dhOu + l2fMuTeEJwdqpSJpq7 + iP8rVej9mM5NQa1n6BTHsp42rDNdSo7 / K4Fz\",\"Timestamp\":1630653484,\"Sign\":\"bd36e2ae3f85dcf32a5084c40fb5a92a\"}";

            CiphertextInfo dataObj = JsonConvert.DeserializeObject<CiphertextInfo>(authorization);
            //tbxResult.Text = dataObj.Dencrypt();



            Guid userId = GetUserID(authorization);
            if (userId != Guid.Empty)
            {
                // 查询用户
                var user = await _userRepository.GetEntityAsync(p => p.ID == userId);
                loginInfo.LoginUser = user;
                //// 查询用户权限
                //V5_DataAuthorities dataAuthority = await _dataAuthorityRepository.GetEntityAsync(p => p.UserOrGroupID == user.ID && p.Type == (int)DataAuthorityLevel.UserLevel
                //                                             && p.IsDeleted == (int)DeleteFlag.NotDelete);
                //loginInfo.DataAuthority = dataAuthority;
            }
            return loginInfo;
        }

        /// <summary>
        /// 根据token获取UserId
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Guid GetUserID(string token)
        {
            Guid userId = Guid.Empty;
            try
            {
                string[] tempArr = token.Split(' ');
                if (tempArr.Length != 2)
                    return userId;

                // 分割Token
                var jwtArr = tempArr[1].Split('.');
                if (jwtArr.Length < 2)
                    return userId;

                //string key = _configurationHelper.GetValue("AESKey");
                //var payLoad = JsonConvert.DeserializeObject<Dictionary<string, string>>(Base64UrlEncoder.Decode(jwtArr[1]));
                //if (!payLoad.ContainsKey("ID"))
                //    return userId;
                // 解密
                //Guid.TryParse(AESEncryptHelper.AESDecrypt(payLoad["ID"], key).Split('|')[0], out userId);
            }
            catch (Exception ex)
            {

            }
            return userId;
        }
    }
}
