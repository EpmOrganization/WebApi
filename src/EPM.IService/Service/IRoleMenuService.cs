using EPM.Model.DbModel;
using EPM.Model.Dto.Response.RoleMenuResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPM.IService.Service
{
    public interface IRoleMenuService
    {
        Task<RoleMenuResponseDto> GetListAsync(Expression<Func<Role, bool>> predicate);
    }
}
