using EPM.IRepository.Base;
using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Dto.Response.WorkItemResponse;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPM.IRepository.Repository
{
    public interface IWorkItemRepository: IBaseRepository<WorkItem>
    {
        Task<WorkItemResponseDto> GetPatgeListAsync(PagingRequest pagingRequest);
    }
}
