using EPM.EFCore.Uow;
using EPM.IRepository.Repository;
using EPM.IService.Service;
using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Dto.Request.WorkItemRequest;
using EPM.Model.Dto.Response.WorkItemResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Service.Service
{
    public class WorkItemService :BaseResult, IWorkItemService
    {
        private readonly IWorkItemRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public WorkItemService(IWorkItemRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }


        public async Task<ValidateResult> AddAsync(WorkItem entity)
        {
            _repository.Add(entity);
            return await _unitOfWork.SaveChangesAsync() > 0 ? BaseResult.ReturnSuccess() : BaseResult.ReturnFail();
        }

        public async Task<IEnumerable<WorkItem>> GetAll()
        {
            return await _repository.GetAllListAsync(null);
        }

        //public async Task<WorkItemResponseDto> GetPatgeListAsync(PagingRequest pagingRequest)
        //{
        //    return await _repository.GetPatgeListAsync(pagingRequest);
        //}

        public async Task<WorkItemResponseDto> GetPageListAsync(WorkItemRequestDto pagingRequest)
        {
            var dto= await _repository.GetListAsync(pagingRequest);
            var newWrokItems = new List<WorkItem>();

            WorkItemResponseDto newResponseDto = new WorkItemResponseDto();
            for (int i = 1; i <= DateTime.DaysInMonth(pagingRequest.Year, pagingRequest.Month); i++)
            {

                var beginDay = DateTime.Parse($"{pagingRequest.Year}-{pagingRequest.Month}-{i} 00:00:00.000");
                var endDay = DateTime.Parse($"{pagingRequest.Year}-{pagingRequest.Month}-{i} 23:59:59.999");
                var oneDayWorkItem = dto.ResponseData.SingleOrDefault(s => s.RecordDate >= beginDay && s.RecordDate <= endDay);
                if (oneDayWorkItem == null)
                {
                    dto.ResponseData.Add(new WorkItem() { RecordDate = beginDay });
                    newWrokItems.Add(new WorkItem() { RecordDate = beginDay });
                }
                //else
                //{
                //    newWrokItems.Add(oneDayWorkItem);
                //}
            }
            return dto;


            //return await _repository.GetListAsync(pagingRequest);
        }

        public async Task<ValidateResult> UpdateAsync(WorkItem entity)
        {
            var workItem = await _repository.GetEntityAsync(p => p.ID == entity.ID);
            // 
            workItem.WorkContent = entity.WorkContent;
            workItem.Description = entity.Description;
            workItem.UpdateTime = DateTime.Now;
            workItem.UpdateUser = "admin";

            Expression<Func<WorkItem, object>>[] updatedProperties =
            {
               p=>p.UpdateUser,
               p=>p.UpdateTime
            };

            _repository.Update(workItem, updatedProperties);
            // 保存数据
            return await _unitOfWork.SaveChangesAsync() > 0 ? ReturnSuccess() : ReturnFail();
        }
    }
}
