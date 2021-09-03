using EPM.IService.Base;
using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPM.IService.Service
{
    public interface IRoleService : IBaseService<Role>
    {
        Task<IEnumerable<Role>> GetPatgeListAsync(PagingRequest pagingRequest);

        Task<IEnumerable<Role>> GetAllListAsync(Expression<Func<Role, bool>> predicate);
    }
}
