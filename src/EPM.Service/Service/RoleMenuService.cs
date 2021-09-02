using EPM.IRepository.Repository;
using EPM.IService.Service;
using EPM.Model.DbModel;
using EPM.Model.Dto.Response.RoleMenuResponse;
using EPM.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Service.Service
{
    public class RoleMenuService : IRoleMenuService
    {
        private readonly IRoleMenuRepository _repository;
        private readonly IRoleRepository _roleRepository;

        public RoleMenuService(IRoleMenuRepository repository,  IRoleRepository roleRepository)
        {
            _repository = repository;
            _roleRepository = roleRepository;
        }

        public async Task<RoleMenuResponseDto> GetListAsync(Expression<Func<Role, bool>> predicate)
        {
            RoleMenuResponseDto list = new RoleMenuResponseDto();

            // 获取该角色信息
            var role = await _roleRepository.GetEntityAsync(predicate);
            list.ClusterID = role.ClusterID;
            list.ID = role.ID;
            list.Name = role.Name;
            list.Description = role.Description;
            list.HalfCheckeds = role.HalfCheckeds;
            list.AllottedMenus = new List<Guid>();
            // 获取角色对应的权限明细信息
            Expression<Func<RoleMenu, bool>> predicateDetail = p => p.RoleID == role.ID && p.IsDeleted == (int)DeleteFlag.NotDeleted;
            var roleAuthorityDetails = await _repository.GetListAsync(predicateDetail);
            foreach (RoleMenu item in roleAuthorityDetails)
            {
                list.AllottedMenus.Add(item.MenuID);
            }
            return list;
        }
    }
}
