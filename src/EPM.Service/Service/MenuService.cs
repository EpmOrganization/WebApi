using EPM.EFCore.Uow;
using EPM.IRepository.Repository;
using EPM.IService.Service;
using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Dto.Response.MenuResponse;
using EPM.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Service.Service
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IRoleMenuRepository _roleMenuRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MenuService(IMenuRepository menuRepository,
            IRoleMenuRepository roleMenuRepository,
            IUnitOfWork unitOfWork)
        {
            _menuRepository = menuRepository;
            _roleMenuRepository = roleMenuRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ValidateResult> AddAsync(Menu entity)
        {
            ValidateResult validateResult = new ValidateResult();

                //LoginInfo loginInfo = await _tokenService.GetLoginInfoByToken();
                entity.ID = Guid.NewGuid();
            //entity.CreateUser = loginInfo.LoginUser.Name;
            //entity.UpdateUser = loginInfo.LoginUser.Name;
            entity.CreateUser = entity.UpdateUser="admin";
            entity.UpdateTime = DateTime.Now;
                entity.CreateTime = DateTime.Now;
            _menuRepository.Add(entity);
            return await _unitOfWork.SaveChangesAsync() > 0 ? BaseResult.ReturnSuccess() : BaseResult.ReturnFail();
        }

        public async Task<ValidateResult> DeleteAsync(Guid id)
        {
            // 判断该菜单是否已经分配给角色
            var details = await _roleMenuRepository.GetAllListAsync(p => p.MenuID == id && p.IsDeleted == (int)DeleteFlag.NotDeleted);
            if (details.Any())
            {
                return BaseResult.MenuHasBeenAssigned();
            }
            else
            {
                // 从传递的token中获取用户信息
                //LoginInfo loginInfo = await _tokenService.GetLoginInfoByToken();
                // 删除权限
                Menu actionAuthority = await _menuRepository.GetEntityAsync(p => p.ID == id && p.IsDeleted == (int)DeleteFlag.NotDeleted);
                actionAuthority.IsDeleted = (int)DeleteFlag.Deleted;
                actionAuthority.UpdateTime = DateTime.Now;
                actionAuthority.UpdateUser = "admin";
                Expression<Func<Menu, object>>[] updatedProperties =
                {
                    p=>p.IsDeleted,
                    p=>p.UpdateTime,
                    p=>p.UpdateUser
                };
                _menuRepository.Update(actionAuthority, updatedProperties);

                return await _unitOfWork.SaveChangesAsync() > 0 ? BaseResult.ReturnSuccess() : BaseResult.ReturnFail();
            }
        }

        public async Task<IEnumerable<MenuResponseDto>> GetList()
        {
            // 获取所有菜单
            var allListMenu = await _menuRepository.GetAllListAsync(null);
            // 获取父级菜单
            var parentList = allListMenu.Where(p => p.ParentMenuID == null);
            List<MenuResponseDto> list = new List<MenuResponseDto>();
            // 遍历父级节点
            foreach (var node in parentList)
            {
                MenuResponseDto dto = new MenuResponseDto();
                dto.ID = node.ID;
                dto.Description = node.Description;
                dto.ID = node.ID;
                dto.Name = node.Name;
                dto.ParentMenuID = node.ParentMenuID;
                dto.Type = node.Type;
                 dto.CreateTime = node.CreateTime;
                 dto.CreateUser = node.CreateUser;
                 dto.UpdateTime = node.UpdateTime;
                 dto.UpdateUser = node.UpdateUser;
                dto.ClusterID = node.ClusterID;
                dto.ParentList = node.ParentList;
                 dto.Value = node.Value;
                BuildTree(dto, allListMenu.ToList());
                list.Add(dto);
            }

            return list;
        }

        /// <summary>
        /// 递归构建Children
        /// </summary>
        /// <param name="paraParentNode"></param>
        /// <returns></returns>
        private void BuildTree(MenuResponseDto paraParentNode, List<Menu> listMenu)
        {
            // 获取子级
            var nodes = listMenu.Where(p => p.ParentMenuID == paraParentNode.ID && p.IsDeleted == (int)DeleteFlag.NotDeleted);

            if (nodes.Any() && paraParentNode.ChildrenMenu == null)
                paraParentNode.ChildrenMenu = new List<MenuResponseDto>();
            // 循环遍历子级
            foreach (Menu node in nodes)
            {
                MenuResponseDto dto = new MenuResponseDto();
                dto.ID = node.ID;
                dto.Description = node.Description;
                dto.ID = node.ID;
                dto.Name = node.Name;
                dto.ParentMenuID = node.ParentMenuID;
                dto.Type = node.Type;
                dto.CreateTime = node.CreateTime;
                dto.CreateUser = node.CreateUser;
                dto.UpdateTime = node.UpdateTime;
                dto.UpdateUser = node.UpdateUser;
                dto.ClusterID = node.ClusterID;
                dto.ParentList = node.ParentList;
                dto.Value = node.Value;

                paraParentNode.ChildrenMenu.Add(dto);
                BuildTree(dto,listMenu);
            }
        }

        public Task<ValidateResult> UpdateAsync(Menu entity)
        {
            throw new NotImplementedException();
        }
    }
}
