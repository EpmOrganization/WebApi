using EPM.IService.Base;
using EPM.Model.DbModel;
using EPM.Model.Dto.Response.DeptResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPM.IService.Service
{
    public interface IDepartmentService : IBaseService<Department>
    {
        Task<IEnumerable<DeptResponseDto>> GetList();
    }
}
