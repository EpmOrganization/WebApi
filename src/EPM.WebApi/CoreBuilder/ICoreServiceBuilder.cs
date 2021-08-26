using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.WebApi.CoreBuilder
{
    public interface ICoreServiceBuilder
    {
        /// <summary>
        /// 添加Swagger生成器
        /// </summary>
        void AddSwaggerGenerator();

        /// <summary>
        /// 添加数据库连接配置
        /// </summary>
        void AddDbConfig();
    }
}
