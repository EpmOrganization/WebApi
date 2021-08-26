using EPM.IRepository.Base;
using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using System.Threading.Tasks;

namespace EPM.IRepository.Repository
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Task<Role> GetPatgeListAsync(PagingRequest pagingRequest);
    }
}
