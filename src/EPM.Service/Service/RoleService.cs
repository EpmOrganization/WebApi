using EPM.EFCore.Uow;
using EPM.IRepository.Repository;
using EPM.IService.Service;
using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Service.Service
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IRoleRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }


        public Task<ValidateResult> AddAsync(Role entity)
        {
            throw new NotImplementedException();
        }

        public Task<ValidateResult> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Role>> GetPatgeListAsync(PagingRequest pagingRequest)
        {
            return await _repository.GetPatgeListAsync(pagingRequest);
        }

        public Task<ValidateResult> UpdateAsync(Role entity)
        {
            throw new NotImplementedException();
        }
    }
}
