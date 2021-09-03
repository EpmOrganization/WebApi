﻿using EPM.IService.Service;
using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service;
        }

        /// <summary>
        /// 分页获取所有角色信息
        /// </summary>
        /// <param name="searchRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getlist")]
        public async Task<ActionResult<ApiResponseWithData<List<Role>>>> Getlist([FromBody] PagingRequest pagingRequest)
        {
            ApiResponseWithData<List<Role>> result = new ApiResponseWithData<List<Role>>().Success();

            var responseDto = await _service.GetPatgeListAsync(pagingRequest);
            if (responseDto != null)
            {
                result.Data = responseDto.ToList();
                result.Count = responseDto.Count();
            }
            else
            {
                result = result.Fail();
            }
            return result;
        }

        /// <summary>
        /// 获取所有角色信息列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponseWithData<List<Role>>>> Get()
        {
            ApiResponseWithData<List<Role>> result = new ApiResponseWithData<List<Role>>().Success();

            var responseDto = await _service.GetAllListAsync(p=>p.IsDeleted==(int)DeleteFlag.NotDeleted);
            if (responseDto != null)
            {
                result.Data = responseDto.ToList();
                result.Count = responseDto.Count();
            }
            else
            {
                result = result.Fail();
            }

            return result;
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Post([FromBody] Role role)
        {
            ValidateResult validateResult = await _service.AddAsync(role);
            return validateResult.Code > 0 ? ApiResponse.Success() : ApiResponse.Fail();
        }

        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<ApiResponse>> Put([FromBody] Role role)
        {
            ValidateResult validateResult = await _service.UpdateAsync(role);
            return validateResult.Code > 0 ? ApiResponse.Success() : ApiResponse.Fail();
        }


        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            ValidateResult validateResult = await _service.DeleteAsync(id);
            return validateResult.Code > 0 ? ApiResponse.Success() : ApiResponse.Fail();
        }
    }
}
