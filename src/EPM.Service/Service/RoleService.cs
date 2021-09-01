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
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;
        private readonly IRoleMenuRepository _roleMenuRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IRoleRepository repository, IRoleMenuRepository roleMenuRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _roleMenuRepository = roleMenuRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<ValidateResult> AddAsync(Role entity)
        {
            // 添加角色信息
            // 判断是否已经存在同名角色
            var role = await _repository.GetEntityAsync(p => p.Name == entity.Name && p.IsDeleted == (int)DeleteFlag.NotDeleted);
            if (null != role)
            {
                //validateResult.ValidateCode = 0;
                //validateResult.ValidateMsg = "已存在同名角色，请勿重复添加";
                return new ValidateResult()
                {
                    Code = 0,
                    Msg = "已存在同名角色，请勿重复添加"
                };
            }
            else
            {
                // 从传递的token中获取用户信息
                //LoginInfo loginInfo = await _tokenService.GetLoginInfoByToken();
                entity.CreateTime = entity.UpdateTime = DateTime.Now;
                //entity.CreateUser = entity.UpdateUser = loginInfo.LoginUser.Name;
                entity.CreateUser = entity.UpdateUser = "admin";
                entity.ID = Guid.NewGuid();
                // 添加Role
                _repository.Add(entity);

                // 添加角色对应菜单权限信息
                if (entity.AllottedRoles != null && entity.AllottedRoles.Length > 0)
                {
                    RoleMenu detail = null;
                    foreach (Guid authorityId in entity.AllottedRoles)
                    {
                        DateTime dt = DateTime.Now;
                        detail = new RoleMenu()
                        {
                            RoleID = entity.ID,
                            MenuID = authorityId,
                            CreateTime = dt,
                            //CreateUser = loginInfo.LoginUser.Name,
                            CreateUser = "admin",
                            ID = Guid.NewGuid(),
                            UpdateTime = dt,
                            //UpdateUser = loginInfo.LoginUser.Name
                            UpdateUser = "admin"
                        };
                        // 添加Role对应权限信息
                        _roleMenuRepository.Add(detail);
                    }
                }
                // 通过事务保存数据
                return await _unitOfWork.SaveChangesAsync() > 0 ? BaseResult.ReturnSuccess() : BaseResult.ReturnFail();
            }
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ValidateResult> DeleteAsync(Guid id)
        {
            //// 从Token获取当前登录用户信息
            //LoginInfo loginInfo = await _tokenService.GetLoginInfoByToken();
            // 判断该角色是否已经分配给用户使用
            var users = await _userRepository.GetAllListAsync(p => p.RoleID == id);
            // 有数据
            if (users.Any())
            {
                return BaseResult.RoleAllot();
            }
            else
            {
                // 获取角色
                var role = await _repository.GetEntityAsync(p => p.ID == id);

                role.IsDeleted = (int)DeleteFlag.Deleted;
                role.UpdateTime = DateTime.Now;
                //role.UpdateUser = loginInfo.LoginUser.Name;
                role.UpdateUser = "admin";
                Expression<Func<Role, object>>[] updatedProperties =
                {
                    p=>p.IsDeleted,
                    p=>p.UpdateTime,
                    p=>p.UpdateUser
                };
                _repository.Update(role, updatedProperties);

                // 获取角色对应菜单权限明细
                RoleMenu[] details = (await _roleMenuRepository.GetAllListAsync(p => p.RoleID == id)).ToArray();
                if (details.Length > 0)
                {
                    foreach (RoleMenu item in details)
                    {
                        item.IsDeleted = (int)DeleteFlag.Deleted;
                        item.UpdateTime = DateTime.Now;
                        //item.UpdateUser = loginInfo.LoginUser.Name;
                        item.UpdateUser = "admin";
                        Expression<Func<RoleMenu, object>>[] updatedPropertiesDetail =
                        {
                            p=>p.IsDeleted,
                            p=>p.UpdateTime,
                            p=>p.UpdateUser
                        };
                        _roleMenuRepository.Update(item, updatedPropertiesDetail);

                    }

                }

                return await _unitOfWork.SaveChangesAsync() > 0 ? BaseResult.ReturnSuccess() : BaseResult.ReturnFail();
            }
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
