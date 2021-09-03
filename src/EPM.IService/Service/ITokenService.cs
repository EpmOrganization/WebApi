using EPM.Model.ApiModel;
using System;
using System.Threading.Tasks;

namespace EPM.IService.Service
{
    public interface ITokenService
    {
        Task<LoginInfo> GetLoginInfoByToken();

        Guid GetUserID(string token);
    }
}
