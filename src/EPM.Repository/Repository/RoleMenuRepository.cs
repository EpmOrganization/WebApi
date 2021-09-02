using EPM.EFCore.Context;
using EPM.IRepository.Repository;
using EPM.Model.DbModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EPM.Repository.Repository
{
    public class RoleMenuRepository : IRoleMenuRepository
    {
        private readonly AppDbContext _appDbContext;

        public RoleMenuRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public void AddBatch(List<RoleMenu> list)
        {
            _appDbContext.RoleMenus.AddRange(list.ToArray());
        }

        public async Task<IEnumerable<RoleMenu>> GetListAsync(Expression<Func<RoleMenu, bool>> predicate)
        {
            return await _appDbContext.RoleMenus.Where(predicate).ToListAsync();
        }

        public void Update(RoleMenu entity, Expression<Func<RoleMenu, object>>[] updatedProperties)
        {
            throw new NotImplementedException();
        }
    }
}
