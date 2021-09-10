using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Dto.Request.WorkItemRequest;
using EPM.Model.Dto.Response.WorkItemResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPM.IService.Service
{
    public interface IWorkItemService
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ValidateResult> AddAsync(WorkItem entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ValidateResult> UpdateAsync(WorkItem entity);

        Task<WorkItemResponseDto> GetPageListAsync(WorkItemRequestDto pagingRequest);

        Task<IEnumerable<WorkItem>> GetAll();

        Task<WorkItemResponseDto> GetAuthorityListAsync(AuthorityWorkItemRequestDto pagingRequest);
    }
}
