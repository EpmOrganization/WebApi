using EPM.EFCore.Context;
using EPM.IRepository.Repository;
using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Dto.Request.WorkItemRequest;
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

        public async Task<WorkItemResponseDto> GetPageListAsync(PagingRequest pagingRequest)
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


        public async Task<WorkItemResponseDto> GetListAsync(WorkItemRequestDto pagingRequest)
        {
            WorkItemResponseDto responseDto = new WorkItemResponseDto();
            pagingRequest.Year = pagingRequest.Year == 0 ? DateTime.Now.Year : pagingRequest.Year;
            pagingRequest.Month = pagingRequest.Month == 0 ? DateTime.Now.Month : pagingRequest.Month;
            var beginDT = DateTime.Parse($"{pagingRequest.Year}-{pagingRequest.Month}-01 00:00:00.000");
            var endDT = DateTime.Parse($"{pagingRequest.Year}-{pagingRequest.Month}-{DateTime.DaysInMonth(pagingRequest.Year, pagingRequest.Month)} 23:59:59.999");
            var query = from u in _dbContext.WorkItems
                        where u.RecordDate >=beginDT && u.RecordDate<=endDT
                        select new WorkItem
                        {
                            ClusterID = u.ClusterID,
                            ID = u.ID,
                            CreateTime = u.CreateTime,
                            CreateUser = u.CreateUser,
                            UpdateTime = u.UpdateTime,
                            UpdateUser = u.UpdateUser,
                            Description = u.Description,
                            WorkContent = u.WorkContent,
                            RecordDate = u.RecordDate
                        };

            // 分页查询
            if (pagingRequest.IsPaging)
            {
                var skip = (pagingRequest.PageIndex - 1) * pagingRequest.PageSize;
                responseDto.ResponseData = await query
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
