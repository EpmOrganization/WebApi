using EPM.IRepository.Base;
using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Dto.Response.UserResponse;
using System.Threading.Tasks;

namespace EPM.IRepository.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<UserResponseDto> GetPatgeListAsync(PagingRequest pagingRequest);
    }
}
