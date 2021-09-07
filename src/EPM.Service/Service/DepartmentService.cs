﻿using EPM.EFCore.Uow;
using EPM.IRepository.Repository;
using EPM.IService.Service;
using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Dto.Response.DeptResponse;
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
        private readonly ITokenService _tokenService;

        public DepartmentService(IDepartmentRepository repository, IUnitOfWork unitOfWork,
            ITokenService tokenService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<ValidateResult> AddAsync(Department entity)
        {

            // 判断是否已存在相同名称的部门
            var dept = await _repository.GetEntityAsync(p => p.Name == entity.Name && p.IsDeleted==(int)DeleteFlag.NotDeleted);
            if(null !=dept)
            {
                return BaseResult.ReturnFail();
            }
            else
            {
                // 从传递的token中获取用户信息
                LoginInfo loginInfo = await _tokenService.GetLoginInfoByToken();
                entity.ID = Guid.NewGuid();
                entity.CreateTime = entity.UpdateTime = DateTime.Now;
                entity.CreateUser = entity.UpdateUser = loginInfo.LoginUser.Name;
                _repository.Add(entity);
                return await _unitOfWork.SaveChangesAsync() > 0 ? BaseResult.ReturnSuccess() : BaseResult.ReturnFail();
            }
        }

        public async Task<ValidateResult> DeleteAsync(Guid id)
        {
            var dept = await _repository.GetEntityAsync(p => p.ID == id);
            if(null !=dept)
            {
                // 从传递的token中获取用户信息
                LoginInfo loginInfo = await _tokenService.GetLoginInfoByToken();
                dept.IsDeleted = (int)DeleteFlag.Deleted;
                dept.UpdateTime = DateTime.Now;
                dept.UpdateUser = loginInfo.LoginUser.Name;
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

        /// <summary>
        /// 递归构建Children
        /// </summary>
        /// <param name="paraParentNode"></param>
        /// <returns></returns>
        private void BuildTree(DeptResponseDto paraParentNode, List<Department> listMenu)
        {
            // 获取子级
            var nodes = listMenu.Where(p => p.ParentID == paraParentNode.ID && p.IsDeleted == (int)DeleteFlag.NotDeleted);

            if (nodes.Any() && paraParentNode.Children == null)
                paraParentNode.Children = new List<DeptResponseDto>();
            // 循环遍历子级
            foreach (Department node in nodes)
            {
                DeptResponseDto dto = new DeptResponseDto();
                dto.ID = node.ID;
                dto.Description = node.Description;
                dto.ID = node.ID;
                dto.Name = node.Name;
                dto.ParentID = node.ParentID;
                dto.CreateTime = node.CreateTime;
                dto.CreateUser = node.CreateUser;
                dto.UpdateTime = node.UpdateTime;
                dto.UpdateUser = node.UpdateUser;
                dto.ClusterID = node.ClusterID;
                dto.Dep = node.Dep;
                paraParentNode.Children.Add(dto);
                BuildTree(dto, listMenu);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<ValidateResult> UpdateAsync(Department entity)
        {
            var organization = await _repository.GetEntityAsync(p => p.Name == entity.Name && p.IsDeleted == (int)DeleteFlag.NotDeleted && p.ID != entity.ID);
            if (organization != null)
            {
                return BaseResult.ExistDepartment();
            }
            else
            {
                // 从传递的token中获取用户信息
                LoginInfo loginInfo = await _tokenService.GetLoginInfoByToken();
                // 查询选择的部门
                var dept = await _repository.GetEntityAsync(p => p.ID == entity.ID && p.IsDeleted == (int)DeleteFlag.NotDeleted);
                dept.Name = entity.Name;
                dept.ParentID = entity.ParentID;
                dept.Description = entity.Description;
                dept.UpdateTime = DateTime.Now;
                dept.UpdateUser = loginInfo.LoginUser.Name;
                Expression<Func<Department, object>>[] updatedDataAuthority =
                {
                    p=>p.UpdateTime,
                    p=>p.UpdateUser,
                    p=>p.Description,
                    p=>p.ParentID,
                    p=>p.Name
                };
                _repository.Update(dept, updatedDataAuthority);
                return await _unitOfWork.SaveChangesAsync() > 0 ? BaseResult.ReturnSuccess() : BaseResult.ReturnFail();
            }

        }

        public async Task<IEnumerable<DeptResponseDto>> GetList()
        {
            // 获取所有有效的部门
            var allListMenu = await _repository.GetAllListAsync(p=>p.IsDeleted==(int)DeleteFlag.NotDeleted);
            // 获取父级菜单
            var parentList = allListMenu.Where(p => p.ParentID == null);
            List<DeptResponseDto> list = new List<DeptResponseDto>();
            // 遍历父级节点
            foreach (var node in parentList)
            {
                DeptResponseDto dto = new DeptResponseDto();
                dto.ID = node.ID;
                dto.Description = node.Description;
                dto.ID = node.ID;
                dto.Name = node.Name;
                dto.ParentID = node.ParentID;
                dto.CreateTime = node.CreateTime;
                dto.CreateUser = node.CreateUser;
                dto.UpdateTime = node.UpdateTime;
                dto.UpdateUser = node.UpdateUser;
                dto.ClusterID = node.ClusterID;
                dto.Dep = node.Dep;
                BuildTree(dto, allListMenu.ToList());
                list.Add(dto);
            }

            return list;
        }
    }
}
