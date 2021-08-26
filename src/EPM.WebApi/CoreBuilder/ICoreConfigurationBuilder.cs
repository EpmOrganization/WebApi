using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.WebApi.CoreBuilder
{
    public interface ICoreConfigurationBuilder
    {
        /// <summary>
        /// Swagger配置
        /// </summary>
        void UseSwagger();
    }
}
