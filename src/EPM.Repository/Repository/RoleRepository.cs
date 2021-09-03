using EPM.EFCore.Context;
using EPM.IRepository.Repository;
using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Enum;
using EPM.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Repository.Repository
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext dbContext)
              : base(dbContext, dbContext.Roles)
        {
        }

        public async Task<IEnumerable<Role>> GetPatgeListAsync(PagingRequest pagingRequest)
        {
            var query = from r in _dbContext.Roles
                        where r.IsDeleted == (int)DeleteFlag.NotDeleted
                        select new Role
                        {
                            Name=r.Name,
                            ID=r.ID,
                            CreateTime=r.CreateTime,
                            CreateUser=r.CreateUser,
                            UpdateTime=r.UpdateTime,
                            UpdateUser=r.UpdateUser
                        };
            // 分页查询
            if (pagingRequest.IsPaging)
            {
                var skip = (pagingRequest.PageIndex - 1) * pagingRequest.PageSize;
                return await query
                    .Skip(skip).Take(pagingRequest.PageSize).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }
    }
}
