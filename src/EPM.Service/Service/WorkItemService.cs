using EPM.EFCore.Uow;
using EPM.IRepository.Repository;
using EPM.IService.Service;
using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Dto.Request.WorkItemRequest;
using EPM.Model.Dto.Response.WorkItemResponse;
using EPM.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Service.Service
{
    public class WorkItemService : BaseResult, IWorkItemService
    {
        private readonly IWorkItemRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public WorkItemService(IWorkItemRepository repository, IUnitOfWork unitOfWork,
            ITokenService tokenService,
            IUserRepository userRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _userRepository= userRepository;    
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
            return await _unitOfWork.SaveChangesAsync() > 0 ? ReturnSuccess() : ReturnFail();
        }

        public async Task<IEnumerable<WorkItem>> GetAll()
        {
            return await _repository.GetAllListAsync(null);
        }

        public async Task<WorkItemResponseDto> GetAuthorityListAsync(AuthorityWorkItemRequestDto pagingRequest)
        {
            // 从传递的token中获取用户信息
            LoginInfo loginInfo = await _tokenService.GetLoginInfoByToken();
            string[] selectedDate = pagingRequest.SelectedDate.Split('-');
            int year = Convert.ToInt32(selectedDate[0]);
            int month = Convert.ToInt32(selectedDate[1]);

            WorkItemResponseDto dto = new WorkItemResponseDto();
            dto.ResponseData = new List<WorkItem>();
            if (pagingRequest.DepartID != null)
            {
                // 查询部门下面某一个用户的工作记录
                if (pagingRequest.DepartID.Count == 2)
                {
                    SearchCondition search = new SearchCondition()
                    {
                        Year = year,
                        Month = month,
                        UserID = loginInfo.LoginUser.ID

                    };
                    dto = await _repository.GetListAsync(search);
                }
                else
                {
                    // 查询整个部门的工作记录
                    // 
                    var departId = pagingRequest.DepartID[0];
                    // 获取部门下面的员工
                    var users = await _userRepository.GetAllListAsync(p => p.DepartmentID == departId && p.IsDeleted == (int)DeleteFlag.NotDeleted);
                    foreach (var item in users)
                    {
                        SearchCondition search = new SearchCondition()
                        {
                            Year = year,
                            Month = month,
                            UserID = item.ID

                        };
                        var temp = await _repository.GetListAsync(search);
                       
                        dto.ResponseData.AddRange(temp.ResponseData);
                    }
                }
            }
            else
            {
                // 查询当前登录用户的工作记录
                SearchCondition search = new SearchCondition()
                {
                    Year = year,
                    Month = month,
                    UserID = loginInfo.LoginUser.ID

                };
                dto = await _repository.GetListAsync(search);
            }

            if(dto != null && dto.ResponseData.Count>0)
            {
                var newWrokItems = new List<WorkItem>();

                WorkItemResponseDto newResponseDto = new WorkItemResponseDto();
                for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
                {

                    var beginDay = DateTime.Parse($"{year}-{month}-{i} 00:00:00.000");
                    var endDay = DateTime.Parse($"{year}-{month}-{i} 23:59:59.999");
                    var oneDayWorkItem = dto.ResponseData.SingleOrDefault(s => s.RecordDate >= beginDay && s.RecordDate <= endDay);
                    if (oneDayWorkItem == null)
                    {
                        dto.ResponseData.Add(new WorkItem() { RecordDate = beginDay, IsRecord = 0 });
                        newWrokItems.Add(new WorkItem() { RecordDate = beginDay });
                    }
                }
            }
            return dto;
        }

        public async Task<WorkItemResponseDto> GetPageListAsync(WorkItemRequestDto pagingRequest)
        {
            // 从传递的token中获取用户信息
            LoginInfo loginInfo = await _tokenService.GetLoginInfoByToken();
            string[] selectedDate = pagingRequest.SelectedDate.Split('-');
            int year = Convert.ToInt32(selectedDate[0]);
            int month = Convert.ToInt32(selectedDate[1]);

            SearchCondition search = new SearchCondition()
            {
                Year = year,
                Month = month,
                UserID = loginInfo.LoginUser.ID
            };

            var dto = await _repository.GetListAsync(search);
            var newWrokItems = new List<WorkItem>();

            WorkItemResponseDto newResponseDto = new WorkItemResponseDto();
            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {

                var beginDay = DateTime.Parse($"{year}-{month}-{i} 00:00:00.000");
                var endDay = DateTime.Parse($"{year}-{month}-{i} 23:59:59.999");
                var oneDayWorkItem = dto.ResponseData.SingleOrDefault(s => s.RecordDate >= beginDay && s.RecordDate <= endDay);
                if (oneDayWorkItem == null)
                {
                    dto.ResponseData.Add(new WorkItem() { RecordDate = beginDay, IsRecord = 0 });
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
