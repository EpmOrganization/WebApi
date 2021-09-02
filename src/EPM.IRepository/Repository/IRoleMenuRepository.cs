using EPM.IRepository.Base;
using EPM.Model.DbModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EPM.IRepository.Repository
{
    //public interface IRoleMenuRepository : IBaseRepository<RoleMenu>
    //{

    //}

    public interface IRoleMenuRepository 
    {
        Task<IEnumerable<RoleMenu>> GetListAsync(Expression<Func<RoleMenu, bool>> predicate);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>

        void AddBatch(List<RoleMenu> list);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Update(RoleMenu entity, Expression<Func<RoleMenu, object>>[] updatedProperties);
    }
}
