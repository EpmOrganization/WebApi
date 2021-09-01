using EPM.EFCore.Uow;
using EPM.IRepository.Repository;
using EPM.IService.Service;
using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Service.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IDepartmentRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ValidateResult> AddAsync(Department entity)
        {
            // 判断是否已存在相同名称的部门
            var dept = await _repository.GetEntityAsync(p => p.Name == entity.Name);
            if(null !=dept)
            {
                return BaseResult.ReturnFail();
            }
            else
            {
                entity.ID = Guid.NewGuid();
                entity.CreateTime = entity.UpdateTime = DateTime.Now;
                entity.CreateUser = entity.UpdateUser = "admin";
                _repository.Add(entity);
                return await _unitOfWork.SaveChangesAsync() > 0 ? BaseResult.ReturnSuccess() : BaseResult.ReturnFail();
            }
        }

        public async Task<ValidateResult> DeleteAsync(Guid id)
        {
            var dept = await _repository.GetEntityAsync(p => p.ID == id);
            if(null !=dept)
            {
                dept.IsDeleted = (int)DeleteFlag.Deleted;
                dept.UpdateTime = DateTime.Now;
                dept.UpdateUser = "admin";
                Expression<Func<Department, object>>[] updatedProperties =
                {
                    p=>p.IsDeleted,
                    p=>p.UpdateTime,
                    p=>p.UpdateUser
                };
                _repository.Update(dept, updatedProperties);
                return await _unitOfWork.SaveChangesAsync() > 0 ? BaseResult.ReturnSuccess() : BaseResult.ReturnFail();

            }
            else
            {
                return new ValidateResult()
                {
                    Code = 0,
                    Msg = "删除失败，未找到要删除的部门"
                };
            }
        }

        public async Task<IEnumerable<Department>> GetAllListAsync(Expression<Func<Department, bool>> predicate)
        {
            return await _repository.GetAllListAsync(predicate);
        }

        public Task<ValidateResult> UpdateAsync(Department entity)
        {
            throw new NotImplementedException();
        }
    }
}
