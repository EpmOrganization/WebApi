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
        private readonly ITokenService _tokenService;

        public WorkItemService(IWorkItemRepository repository, IUnitOfWork unitOfWork,
            ITokenService tokenService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }


        public async Task<ValidateResult> AddAsync(WorkItem entity)
        {
            // 从传递的token中获取用户信息
            LoginInfo loginInfo = await _tokenService.GetLoginInfoByToken();
            entity.CreateUser = entity.UpdateUser = loginInfo.LoginUser.Name;
            entity.CreateTime = entity.UpdateTime = DateTime.Now;
            entity.CreateUserID = loginInfo.LoginUser.ID;
            entity.IsRecord = 1;
            _repository.Add(entity);
            return await _unitOfWork.SaveChangesAsync() > 0 ? ReturnSuccess():ReturnFail();
        }

        public async Task<IEnumerable<WorkItem>> GetAll()
        {
            return await _repository.GetAllListAsync(null);
        }

        public async Task<WorkItemResponseDto> GetPageListAsync(WorkItemRequestDto pagingRequest)
        {
            // 从传递的token中获取用户信息
            LoginInfo loginInfo = await _tokenService.GetLoginInfoByToken();
            pagingRequest.UserID = loginInfo.LoginUser.ID;
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
                    dto.ResponseData.Add(new WorkItem() { RecordDate = beginDay,IsRecord=0 });
                    newWrokItems.Add(new WorkItem() { RecordDate = beginDay });
                }
            }
            return dto;
        }

        public async Task<ValidateResult> UpdateAsync(WorkItem entity)
        {
            // 从传递的token中获取用户信息
            LoginInfo loginInfo = await _tokenService.GetLoginInfoByToken();
            var workItem = await _repository.GetEntityAsync(p => p.ID == entity.ID);
            workItem.WorkContent = entity.WorkContent;
            workItem.Description = entity.Description;
            workItem.UpdateTime = DateTime.Now;
            workItem.UpdateUser = loginInfo.LoginUser.Name;

            Expression<Func<WorkItem, object>>[] updatedProperties =
            {
               p=>p.UpdateUser,
               p=>p.UpdateTime,
               p=>p.WorkContent,
               p=>p.Description
            };

            _repository.Update(workItem, updatedProperties);
            // 保存数据
            return await _unitOfWork.SaveChangesAsync() > 0 ? ReturnSuccess() : ReturnFail();
        }
    }
}
