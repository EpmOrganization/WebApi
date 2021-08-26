using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPM.EFCore.Uow
{
    public class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
    {
        private TDbContext _dbContext;
        private bool _disposed;

        public UnitOfWork(TDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 释放数据库资源
        /// </summary>
        /// <param name="disposing"></param>
        public virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;


            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                    _dbContext = null;
                }
            }
            _disposed = true;
        }

        //public Task<int> ExecuteSql(string strSql)
        //{
        //    _dbContext.Database.ExecuteSqlRawAsync(strSql, null);
        //    throw new NotImplementedException();
        //}

        //public async Task<int> ExecuteSql(string strSql, object[] parameters = null)
        //{
        //    int rows = 0;
        //    if (parameters == null)
        //    {
        //        rows = await _dbContext.Database.ExecuteSqlRawAsync(strSql);
        //    }
        //    else
        //    {
        //        rows = await _dbContext.Database.ExecuteSqlRawAsync(strSql, parameters);
        //    }

        //    return rows;
        //}

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
