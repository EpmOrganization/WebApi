using EPM.IRepository.Base;
using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPM.IRepository.Repository
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Task<IEnumerable<Role>> GetPatgeListAsync(PagingRequest pagingRequest);
    }
}
