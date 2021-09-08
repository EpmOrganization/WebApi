using EPM.IService.Base;
using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Dto.Response.UserResponse;
using System.Threading.Tasks;

namespace EPM.IService.Service
{
    public interface IUserService : IBaseService<User>
    {
        Task<UserResponseDto> GetPatgeListAsync(PagingRequest pagingRequest);

        Task<User> GetLoginUser();
    }
}
