using EPM.Model.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPM.IRepository.Repository
{
    public interface IDataAuthorityRepository
    {
        Task<List<DataAuthority>> GetListDtoAsync(Expression<Func<DataAuthority, bool>> predicate);

        void AddBatch(List<DataAuthority> list);

        void DeleteBatch(List<DataAuthority> list);
    }
}
