using EPM.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPM.IService.Service
{
    public interface ILoginService
    {
        Task<LoginStatus> LoginOut();
    }
}
