using EPM.EFCore.Uow;
using EPM.IRepository.Repository;
using EPM.IService.Service;
using EPM.Model.ApiModel;
using EPM.Model.DbModel;
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

        public async Task<WorkItemResponseDto> GetPatgeListAsync(PagingRequest pagingRequest)
        {
            return await _repository.GetPatgeListAsync(pagingRequest);
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
