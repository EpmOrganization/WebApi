
using EPM.Framework.Security;
using EPM.IRepository.Repository;
using EPM.IService.Service;
using EPM.Model.ApiModel;
using EPM.Model.ConfigModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPM.Service.Service
{
    public class TokenService : ITokenService
    {
        private readonly JwtConfig _jwtConfig;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public TokenService(IOptions<JwtConfig> jwtConfig, IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository)
        {
            _jwtConfig = jwtConfig.Value;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public async Task<LoginInfo> GetLoginInfoByToken()
        {
            LoginInfo loginInfo = new LoginInfo();
            var request = _httpContextAccessor.HttpContext.Request;
            string authorization = request.Headers["Authorization"].ToString();
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
        public Guid GetUserID(string authorization)
        {
            Guid userId = Guid.Empty;
            try
            {
                #region 先生成token在加密
                //string[] tempArr = authorization.Split(' ');
                //if (tempArr.Length != 2)
                //    return userId;

                //// 序列化
                //CiphertextInfo dataObj = JsonConvert.DeserializeObject<CiphertextInfo>(tempArr[1]);
                //// 获取解密后的token信息
                //string token = dataObj.Dencrypt();

                //// 分割Token
                //var jwtArr = token.Split('.');
                //if (jwtArr.Length < 2)
                //    return userId;

                //// 解析JWT生成的token信息获取UserID
                //var payLoad = JsonConvert.DeserializeObject<Dictionary<string, string>>(Base64UrlEncoder.Decode(jwtArr[1]));

                //Guid.TryParse(payLoad["ID"], out userId); 
                #endregion

                #region 先加密在生成token
                string[] tempArr = authorization.Split(' ');
                if (tempArr.Length != 2)
                    return userId;

                // 分割Token
                var jwtArr = tempArr[1].Split('.');
                if (jwtArr.Length < 2)
                    return userId;

                // 解析JWT生成的token信息获取UserID
                var payLoad = JsonConvert.DeserializeObject<Dictionary<string, string>>(Base64UrlEncoder.Decode(jwtArr[1]));

                // 序列化
                CiphertextInfo dataObj = JsonConvert.DeserializeObject<CiphertextInfo>(payLoad["ID"]);
                // 获取解密后的token信息
                string token = dataObj.Dencrypt();

                Guid.TryParse(token, out userId);
                #endregion

            }
            catch (Exception ex)
            {

            }
            return userId;
        }
    }
}
