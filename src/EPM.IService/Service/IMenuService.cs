using EPM.IService.Base;
using EPM.Model.DbModel;
using EPM.Model.Dto.Response.MenuResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPM.IService.Service
{
    public interface IMenuService : IBaseService<Menu>
    {
        Task<IEnumerable<MenuResponseDto>> GetList();

        Task<IEnumerable<Menu>> GetAuthorizeAsync();
    }
}
