using EPM.IService.Base;
using EPM.Model.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPM.IService.Service
{
    public interface IDepartmentService : IBaseService<Department>
    {
        ///// <summary>
        ///// 根据条件获取集合数据
        ///// </summary>
        ///// <typeparam name="TKey"></typeparam>
        ///// <param name="predicate"></param>
        ///// <returns></returns>
        Task<IEnumerable<Department>> GetAllListAsync(Expression<Func<Department, bool>> predicate);
    }
}
