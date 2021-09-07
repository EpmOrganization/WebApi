using EPM.Framework.Cache;
using EPM.Model.ConfigModel;
using Microsoft.Extensions.Configuration;
using System.Threading;

namespace EPM.Framework.Settings
{
    /// <summary>
    /// 配置类，单例模式
    /// </summary>
    public class Appsettings
    {
        private static Appsettings single;

        /// <summary>
        /// 单例
        /// </summary>
        /// <returns></returns>
        public static Appsettings Instance()
        {
            if (single != null)
                return single;
            Appsettings temp = new Appsettings();
            Interlocked.CompareExchange(ref single, temp, null);

            return single;
        }

        #region 属性

        /// <summary>
        /// 数据库连接配置
        /// </summary>
        public ConnectionStrings ConnectionStrings { get; private set; }

        public JwtConfig JwtConfig { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AESKey { get; set; }

        private string _RedisConnStr;
        public string RedisConnStr
        {
            get { return _RedisConnStr; }
            set
            {
                _RedisConnStr = value;
                RedisCoreHelper.SetRedisConnStr(value);
            }
        }

        #endregion

        /// <summary>
        /// 初始化配置信息
        /// </summary>
        /// <param name="configuration"></param>
        public void Initaial(IConfiguration configuration)
        {
            ConnectionStrings = new ConnectionStrings()
            {
                DbConnection = "server=10.10.10.78;port=3306;database=epmdb;uid=root;pwd=Hicore_test2020;"
            };
            // 绑定
            configuration.GetSection("ConnectionString").Bind(ConnectionStrings);

            JwtConfig = new JwtConfig()
            {
                // 令牌签发者
                Issuer = "EPM.Authentication",
                // 令牌接收者
                Audience = "EPM.WebApi",
                // 加密key
                IssuerSigningKey = "EPM@AuthenticationP@ssw0rd",
                // 过期时间
                AccessTokenExpiresMinutes = 120
            };
            configuration.GetSection("JwtConfig").Bind(JwtConfig);
        }
    }
}
