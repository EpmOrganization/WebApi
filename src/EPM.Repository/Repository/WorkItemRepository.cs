using EPM.EFCore.Context;
using EPM.IRepository.Repository;
using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Dto.Response.WorkItemResponse;
using EPM.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Repository.Repository
{
    public class WorkItemRepository : BaseRepository<WorkItem>, IWorkItemRepository
    {
        public WorkItemRepository(AppDbContext dbContext)
             : base(dbContext, dbContext.WorkItems)
        {
        }

        public async Task<WorkItemResponseDto> GetPatgeListAsync(PagingRequest pagingRequest)
        {
            WorkItemResponseDto responseDto = new WorkItemResponseDto();
            var query = from u in _dbContext.WorkItems
                        select new WorkItem
                        {
                            ClusterID = u.ClusterID,
                            ID = u.ID,
                            CreateTime = u.CreateTime,
                            CreateUser = u.CreateUser,
                            UpdateTime = u.UpdateTime,
                            UpdateUser = u.UpdateUser,
                            Description = u.Description,
                            WorkContent=u.WorkContent,
                            RecordDate=u.RecordDate
                        };

            // 分页查询
            if (pagingRequest.IsPaging)
            {
                var skip = (pagingRequest.PageIndex - 1) * pagingRequest.PageSize;
                responseDto.ResponseData= await query
                    .Skip(skip).Take(pagingRequest.PageSize).ToListAsync();
            }
            else
            {
                responseDto.ResponseData = await query.ToListAsync();
            }

            responseDto.Count = query.Count();
            return responseDto;
        }
    }
}
