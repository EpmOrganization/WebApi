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
        private readonly ITokenService _tokenService;

        public RoleService(IRoleRepository repository, IRoleMenuRepository roleMenuRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            ITokenService tokenService)
        {
            _repository = repository;
            _roleMenuRepository = roleMenuRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
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
                return new ValidateResult()
                {
                    Code = 0,
                    Msg = "已存在同名角色，请勿重复添加"
                };
            }
            else
            {
                // 从传递的token中获取用户信息
                LoginInfo loginInfo = await _tokenService.GetLoginInfoByToken();
                entity.CreateTime = entity.UpdateTime = DateTime.Now;
                entity.CreateUser = entity.UpdateUser = loginInfo.LoginUser.Name;
                entity.ID = Guid.NewGuid();
                // 添加Role
                _repository.Add(entity);

                // 添加角色对应菜单权限信息
                if (entity.AllottedMenus != null && entity.AllottedMenus.Length > 0)
                {
                    List<RoleMenu> list = new List<RoleMenu>();
                    DateTime dt = DateTime.Now;
                    foreach (Guid authorityID in entity.AllottedMenus)
                    {
                        RoleMenu detail = new RoleMenu()
                        {
                            RoleID = entity.ID,
                            MenuID = authorityID,
                            CreateTime = dt,
                            CreateUser = loginInfo.LoginUser.Name,
                            ID = Guid.NewGuid(),
                            UpdateTime = dt,
                            UpdateUser = loginInfo.LoginUser.Name
                        };
                        list.Add(detail);
                    }
                    _roleMenuRepository.AddBatch(list);
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
            LoginInfo loginInfo = await _tokenService.GetLoginInfoByToken();
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
                role.UpdateUser = loginInfo.LoginUser.Name;
                Expression<Func<Role, object>>[] updatedProperties =
                {
                    p=>p.IsDeleted,
                    p=>p.UpdateTime,
                    p=>p.UpdateUser
                };
                _repository.Update(role, updatedProperties);

                // 获取角色对应菜单权限明细
                RoleMenu[] details = (await _roleMenuRepository.GetListAsync(p => p.RoleID == id)).ToArray();
                if (details.Length > 0)
                {
                    foreach (RoleMenu item in details)
                    {
                        item.IsDeleted = (int)DeleteFlag.Deleted;
                        item.UpdateTime = DateTime.Now;
                        item.UpdateUser = loginInfo.LoginUser.Name;
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

        public async Task<IEnumerable<Role>> GetAllListAsync(Expression<Func<Role, bool>> predicate)
        {
            return await _repository.GetAllListAsync(predicate);
        }

        public async Task<IEnumerable<Role>> GetPatgeListAsync(PagingRequest pagingRequest)
        {
            return await _repository.GetPatgeListAsync(pagingRequest);
        }

        public async Task<ValidateResult> UpdateAsync(Role entity)
        {
            // 从传递的token中获取用户信息
            LoginInfo loginInfo = await _tokenService.GetLoginInfoByToken();

            var role = await _repository.GetEntityAsync(p => p.Name == entity.Name && p.ID != entity.ID && p.IsDeleted == (int)DeleteFlag.NotDeleted);
            if (role != null)
            {
                return BaseResult.ExistRole();
            }
            else
            {
                // 查询角色信息
                role = await _repository.GetEntityAsync(p => p.ID == entity.ID && p.IsDeleted == (int)DeleteFlag.NotDeleted);

                role.Description = entity.Description;
                role.UpdateTime = DateTime.Now;
                role.UpdateUser = loginInfo.LoginUser.Name;
                role.Name = entity.Name;
                role.HalfCheckeds = entity.HalfCheckeds;

                // 更新角色
                Expression<Func<Role, object>>[] up = {
                    p=>p.HalfCheckeds,
                    p=>p.Description,
                    p=>p.Name,
                    p=>p.UpdateTime,
                    p=>p.UpdateUser
                };
                _repository.Update(role, up);

                // 查询角色对应权限明细
                var role_details = await _roleMenuRepository.GetListAsync(t => t.RoleID == entity.ID);
                // 先删除该角色对应的权限明细信息
                if (role_details.Any())
                {
                    foreach (var item in role_details)
                    {
                        item.IsDeleted = (int)DeleteFlag.Deleted;
                        item.UpdateTime = DateTime.Now;
                        item.UpdateUser = loginInfo.LoginUser.Name;
                        Expression<Func<RoleMenu, object>>[] updatedPropertiesDetail =
                        {
                            p=>p.IsDeleted,
                            p=>p.UpdateTime,
                            p=>p.UpdateUser
                        };
                        _roleMenuRepository.Update(item, updatedPropertiesDetail);
                    }
                }

                // 插入新的角色权限明细
                if (entity.AllottedMenus != null && entity.AllottedMenus.Length > 0)
                {
                    List<RoleMenu> list = new List<RoleMenu>();
                    DateTime dt = DateTime.Now;
                    foreach (Guid authorityID in entity.AllottedMenus)
                    {
                        RoleMenu detail = new RoleMenu()
                        {
                            RoleID = entity.ID,
                            MenuID = authorityID,
                            CreateTime = dt,
                            CreateUser = loginInfo.LoginUser.Name,
                            ID = Guid.NewGuid(),
                            UpdateTime = dt,
                            UpdateUser = loginInfo.LoginUser.Name
                        };
                        list.Add(detail);
                    }
                    _roleMenuRepository.AddBatch(list);
                }
                return await _unitOfWork.SaveChangesAsync() > 0 ? BaseResult.ReturnSuccess() : BaseResult.ReturnFail();
            }
        }
    }
}
