using EPM.EFCore.Context;
using EPM.IRepository.Repository;
using EPM.Model.DbModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Repository.Repository
{
    public class DataAuthorityRepository : IDataAuthorityRepository
    {
        private readonly AppDbContext _dbContext;

        public DataAuthorityRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddBatch(List<DataAuthority> list)
        {
            _dbContext.DataAuthorities.AddRange(list);
        }

        public void DeleteBatch(List<DataAuthority> list)
        {
            _dbContext.DataAuthorities.RemoveRange(list);
        }

        public async Task<List<DataAuthority>> GetListDtoAsync(Expression<Func<DataAuthority, bool>> predicate)
        {
            return  predicate != null ? await _dbContext.DataAuthorities.Where(predicate).ToListAsync() : await _dbContext.DataAuthorities.ToListAsync();
        }
    }
}
