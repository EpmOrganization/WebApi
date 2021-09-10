using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Dto.Request.DataAuthority;
using EPM.Model.Dto.Response.DataAuthorityResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPM.IService.Service
{
    public interface IDataAuthorityService
    {
        Task<ValidateResult> AddAsync(DataAuthorityRequestDto entity);

        //Task<IEnumerable<DataAuthority>> GetListAsync(Expression<Func<DataAuthority, bool>> predicate);

        Task<IEnumerable<DataAuthorityResponseDto>> GetListDtoAsync(Guid userId);
    }
}
