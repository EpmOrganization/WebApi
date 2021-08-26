using EPM.Model.ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Framework.TokenHelper
{
    public interface ITokenService
    {
        Task<string> CreateToken(string id);

        Task<LoginInfo> GetLoginInfoByToken();

        Guid GetUserID(string token);
    }
}
