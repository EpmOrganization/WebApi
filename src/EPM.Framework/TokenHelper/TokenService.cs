using EPM.IRepository.Repository;
using EPM.Model.ApiModel;
using EPM.Model.ConfigModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Framework.TokenHelper
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
        /// 创建Token
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        private string CreateTokenString(List<Claim> claims)
        {
            var now = DateTime.Now;
            var expires = now.Add(TimeSpan.FromMinutes(_jwtConfig.AccessTokenExpiresMinutes));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.IssuerSigningKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,//Token发布者
                audience: _jwtConfig.Audience,    //Token接受者
                claims: claims, //携带的负载
                notBefore: now, //当前时间token生成时间
                expires: expires,//过期时间
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        public async Task<string> CreateToken(string id)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("ID", id);

            //携带的负载部分，类似一个键值对
            List<Claim> claims = new List<Claim>();
            //这里我们通过键值对把数据提供给它
            foreach (var item in keyValuePairs)
            {
                claims.Add(new Claim(item.Key, item.Value));
            }
            string token = await Task.Run<string>(() =>
            {
                return CreateTokenString(claims);
            });

            return token;
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
