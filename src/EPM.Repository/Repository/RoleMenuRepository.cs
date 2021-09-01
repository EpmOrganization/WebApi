using EPM.EFCore.Context;
using EPM.IRepository.Repository;
using EPM.Model.DbModel;
using EPM.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Repository.Repository
{
    public class RoleMenuRepository : BaseRepository<RoleMenu>, IRoleMenuRepository
    {
        public RoleMenuRepository(AppDbContext dbContext)
        : base(dbContext, dbContext.RoleMenus)
        {
        }
    }
}
