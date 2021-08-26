using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPM.IRepository.Base
{
    /// <summary>
    /// 泛型接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);

        /// <summary>
        /// 更新、删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Update(TEntity entity, Expression<Func<TEntity, object>>[] updatedProperties);

        /// <summary>
        /// 获取单个实体（条件）
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        Task<TEntity> GetEntityAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据条件获取集合数据
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);
    }
}