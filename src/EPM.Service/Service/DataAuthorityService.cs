﻿using EPM.EFCore.Uow;
using EPM.IRepository.Repository;
using EPM.IService.Service;
using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Dto.Request.DataAuthority;
using EPM.Model.Dto.Response.DataAuthorityResponse;
using EPM.Model.Dto.Response.DeptResponse;
using EPM.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EPM.Service.Service
{
    public class DataAuthorityService : BaseResult, IDataAuthorityService
    {

        private readonly IDataAuthorityRepository _allottingRepository;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDepartmentRepository _departmentRepository;

        public DataAuthorityService(IDataAuthorityRepository allottingRepository,
            ITokenService tokenService,
            IUnitOfWork unitOfWork,
            IDepartmentRepository departmentRepository)
        {
            _allottingRepository = allottingRepository;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _departmentRepository = departmentRepository;   
        }

        public async Task<ValidateResult> AddAsync(DataAuthorityRequestDto entity)
        {
            LoginInfo loginInfo = await _tokenService.GetLoginInfoByToken();
            // 先删除该用户原有数据权限
            var authorityAllottings = await _allottingRepository.GetListDtoAsync(p => p.UserID == entity.UserID && p.IsDeleted == (int)DeleteFlag.NotDeleted);
            _allottingRepository.DeleteBatch(authorityAllottings.ToList());

            List<DataAuthority> list = new List<DataAuthority>();
            // 插入新的数据权限
            if (entity.ParaAuthorities != null)
            {
                foreach (Guid authority in entity.ParaAuthorities)
                {
                    list.Add(new DataAuthority
                    {
                        DepartID = authority,
                        CreateTime = DateTime.Now,
                        CreateUser = loginInfo.LoginUser.Name,
                        Description = "手动分配",
                        ID = Guid.NewGuid(),
                        UpdateTime = DateTime.Now,
                        UpdateUser = loginInfo.LoginUser.Name,
                        UserID = entity.UserID
                    });
                }
                // 添加数据
                _allottingRepository.AddBatch(list);
            }
            return await _unitOfWork.SaveChangesAsync() > 0 ? ReturnSuccess() : ReturnFail();
        }


        public async Task<IEnumerable<DataAuthorityResponseDto>> GetListDtoAsync(Guid userId)
        {
            // 根据用户ID获取获取用户对应的部门权限
            List<DataAuthority> list = await _allottingRepository.GetListDtoAsync(p => p.UserID == userId);
            // 获取所有有效的父级部门
            var allListMenu = await _departmentRepository.GetAllListAsync(p => p.IsDeleted == (int)DeleteFlag.NotDeleted && p.ParentID==null);
            List< DataAuthorityResponseDto > listDto=new List<DataAuthorityResponseDto>();
            // 循环所有部门
            foreach (var dept in allListMenu)
            {
                DataAuthorityResponseDto dto=new DataAuthorityResponseDto();
                // 
                if (list.Any(p=>p.DepartID==dept.ID))
                {
                    dto.Allotted = 1;
                }
                else
                {
                    dto.Allotted=0;
                }
                dto.DeptId = dept.ID;
                dto.DeptName = dept.Name;
                // 递归
                await BuildTree(dto, allListMenu.ToList(), list);
                listDto.Add(dto);
            }

           return listDto;
        }


        private async Task BuildTree(DataAuthorityResponseDto paraParentNode, List<Department> listDept, List<DataAuthority> listAuthority)
        {
            // 获取子级部门
            var nodes = await _departmentRepository.GetAllListAsync(p => p.ParentID == paraParentNode.DeptId);
            if (nodes.Any() && paraParentNode.Children == null)
                paraParentNode.Children = new List<DataAuthorityResponseDto>();
            // 循环遍历子级
            foreach (Department node in nodes)
            {
                DataAuthorityResponseDto dto = new DataAuthorityResponseDto();
                // 
                if (listAuthority.Any(p => p.DepartID == node.ID))
                {
                    dto.Allotted = 1;
                }
                else
                {
                    dto.Allotted = 0;
                }
                dto.DeptId = node.ID;
                dto.DeptName = node.Name;
                paraParentNode.Children.Add(dto);
                // 递归
               await  BuildTree(dto, listDept, listAuthority);

            }
        }
    }
}
