using EPM.EFCore.Uow;
using EPM.Framework.Helper;
using EPM.IRepository.Repository;
using EPM.IService.Service;
using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Dto.Response.UserResponse;
using EPM.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EPM.Service.Service
{
    public class UserService : BaseResult, IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ValidateResult> AddAsync(User entity)
        {
            ValidateResult validateResult = new ValidateResult();
            // 从传递的token中获取用户信息
            //LoginInfo loginInfo = await _tokenService.GetLoginInfoByToken();
            // 判断是否已经存在要新增的用户
            User user = await _repository.GetEntityAsync(p => p.LoginName == entity.LoginName && p.IsDeleted == (int)DeleteFlag.NotDeleted && p.Status == (int)UserStatus.Normal);
            if (user != null)
            {
                validateResult.Code = (int)CustomerCode.UserIsExist;
                validateResult.Msg = EnumHelper.GetEnumDesc(CustomerCode.UserIsExist);
            }
            else
            {
                entity.CreateUser = entity.UpdateUser = "admin";
                entity.Password = MD5Helper.Get32LowerMD5(entity.Password);
                entity.LoginErrorCount = 0;
                _repository.Add(entity);
                // 保存数据
                int count = await _unitOfWork.SaveChangesAsync();
                validateResult = count > 0 ? ReturnSuccess() : ReturnFail();
            }

            return validateResult;
        }

        public async Task<ValidateResult> DeleteAsync(Guid id)
        {
            var user = await _repository.GetEntityAsync(p => p.ID == id);
            // 
            user.IsDeleted = (int)DeleteFlag.Deleted;
            user.UpdateTime = DateTime.Now;
            user.UpdateUser = "admin";

            Expression<Func<User, object>>[] updatedProperties =
            {
               p=>p.Password,
               p=>p.UpdateUser,
               p=>p.UpdateTime
            };

            _repository.Update(user, updatedProperties);
            // 保存数据
           return await _unitOfWork.SaveChangesAsync() > 0 ? ReturnSuccess() : ReturnFail();
        }

        public Task<IEnumerable<User>> GetAllListAsync(Expression<Func<User, bool>> predicate)
        {
            return _repository.GetAllListAsync(predicate);
        }

        public async Task<UserResponseDto> GetPatgeListAsync(PagingRequest pagingRequest)
        {
            return await _repository.GetPatgeListAsync(pagingRequest);
        }

        public async Task<ValidateResult> UpdateAsync(User entity)
        {
            ValidateResult validateResult = new ValidateResult();
            var user = await _repository.GetEntityAsync(p => p.ID == entity.ID);
            user.Name = entity.Name;
            user.MobileNumber = entity.MobileNumber;
            user.Description = entity.Description;
            user.EmailAddress = entity.EmailAddress;
            user.DepartmentID = entity.DepartmentID;
            user.RoleID = entity.RoleID;
            user.UpdateTime = DateTime.Now;
            user.UpdateUser = "admin";
            // 用表达式树，更新部分字段
            Expression<Func<User, object>>[] updatedProperties =
            {
               p=>p.Name,
               p=>p.MobileNumber,
               p=>p.Description,
               p=>p.EmailAddress,
               p=>p.Position,
               p=>p.DepartmentID,
               p=>p.RoleID,
               p=>p.Status,
               p=>p.UpdateUser,
               p=>p.UpdateTime
            };
            _repository.Update(user, updatedProperties);
            // 保存数据
            int count = await _unitOfWork.SaveChangesAsync();
            validateResult = count > 0 ? ReturnSuccess() : ReturnFail();
            return validateResult;
        }

    }
}
