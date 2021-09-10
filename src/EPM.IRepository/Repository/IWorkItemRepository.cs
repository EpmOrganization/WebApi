using EPM.IRepository.Base;
using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Dto.Request.WorkItemRequest;
using EPM.Model.Dto.Response.WorkItemResponse;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPM.IRepository.Repository
{
    public interface IWorkItemRepository: IBaseRepository<WorkItem>
    {
        Task<WorkItemResponseDto> GetPageListAsync(PagingRequest pagingRequest);

        Task<WorkItemResponseDto> GetListAsync(SearchCondition pagingRequest);

        //Task<WorkItemResponseDto> GetAuthorityListAsync(AuthorityWorkItemRequestDto pagingRequest);
    }
}
