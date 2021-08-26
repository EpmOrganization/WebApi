using EPM.EFCore.Context;
using EPM.Framework.ExpressionTree;
using EPM.IRepository.Repository;
using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Dto.Response.UserResponse;
using EPM.Model.Enum;
using EPM.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Repository.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext)
         : base(dbContext, dbContext.Users)
        {
        }


        public async Task<UserResponseDto> GetPatgeListAsync(PagingRequest pagingRequest)
        {
            UserResponseDto responseDto = new UserResponseDto();
            var query = from u in _dbContext.Users
                        join r in _dbContext.Roles on u.RoleID equals r.ID into td
                        from td1 in td.DefaultIfEmpty()
                        join d in _dbContext.Departments on u.DepartmentID equals d.ID into ud
                        from ud1 in ud.DefaultIfEmpty()
                        where u.IsDeleted == (int)DeleteFlag.NotDeleted
                        select new UserDto
                        {
                            ClusterID = u.ClusterID,
                            ID = u.ID,
                            CreateTime = u.CreateTime,
                            CreateUser = u.CreateUser,
                            UpdateTime = u.UpdateTime,
                            UpdateUser = u.UpdateUser,
                            Name = u.Name,
                            LoginName = u.LoginName,
                            Password = u.Password,
                            MobileNumber = u.MobileNumber,
                            EmailAddress = u.EmailAddress,
                            Position = u.Position,
                            Status = u.Status,
                            LoginTime = u.LoginTime,
                            LoginErrorCount = u.LoginErrorCount,
                            RoleName = td1.Name,
                            DepartmentName = ud1.Name,
                            Description = u.Description,
                            IsDeleted = u.IsDeleted
                        };
            switch(pagingRequest.Order)
            {
                case "aesc":
                    // 分页查询
                    if (pagingRequest.IsPaging)
                    {
                        var skip = (pagingRequest.PageIndex - 1) * pagingRequest.PageSize;
                        responseDto.ResponseData = await query.OrderBy(p => GetPropertyValue(p, pagingRequest.SortField))
                            .Skip(skip).Take(pagingRequest.PageSize).ToListAsync();
                    }
                    else
                    {
                        responseDto.ResponseData = await query.OrderBy(p => GetPropertyValue(p, pagingRequest.SortField)).ToListAsync();
                    }
                    break;
                case "descending":
                    // 分页查询
                    if (pagingRequest.IsPaging)
                    {
                        var skip = (pagingRequest.PageIndex - 1) * pagingRequest.PageSize;
                        responseDto.ResponseData = await query.OrderByDescending(p => GetPropertyValue(p, pagingRequest.SortField))
                            .Skip(skip).Take(pagingRequest.PageSize).ToListAsync();
                    }
                    else
                    {
                        responseDto.ResponseData = await query.OrderByDescending(p => GetPropertyValue(p, pagingRequest.SortField)).ToListAsync();
                    }
                    break;
            }

            responseDto.Count = query.Count();
            return responseDto;
        }

        private static object GetPropertyValue(object obj,string property)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(property);
            return propertyInfo.GetValue(obj, null);
        }
    }
}
