﻿using EPM.IService.Service;
using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Dto.Request.WorkItemRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPM.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkItemController : ControllerBase
    {
        private readonly IWorkItemService _service;

        public WorkItemController(IWorkItemService service)
        {
            _service = service;
        }


        /// <summary>
        /// 分页获取所有用户信息
        /// </summary>
        /// <param name="searchRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getlist")]
        public async Task<ActionResult<ApiResponseWithData<List<WorkItem>>>> Getlist([FromBody] WorkItemRequestDto pagingRequest)
        {
            ApiResponseWithData<List<WorkItem>> result = new ApiResponseWithData<List<WorkItem>>().Success();

            var responseDto = await _service.GetPageListAsync(pagingRequest);
            if (responseDto != null)
            {
                result.Data = responseDto.ResponseData;
                result.Count = responseDto.Count;
            }
            else
            {
                result = result.Fail();
            }
            return result;
        }


        ///// <summary>
        ///// 分页获取所有用户信息
        ///// </summary>
        ///// <param name="searchRequestDto"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("getlist")]
        //public async Task<ActionResult<ApiResponseWithData<List<WorkItem>>>> Getlist()
        //{
        //    ApiResponseWithData<List<WorkItem>> result = new ApiResponseWithData<List<WorkItem>>().Success();

        //    var responseDto = await _service.GetAll();
        //    if (responseDto != null)
        //    {
        //        result.Data = responseDto.ToList();
        //        result.Count = responseDto.Count();
        //    }
        //    else
        //    {
        //        result = result.Fail();
        //    }
        //    return result;
        //}

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Post([FromBody] WorkItem workItem)
        {
            ValidateResult validateResult = await _service.AddAsync(workItem);
            return validateResult.Code > 0 ? ApiResponse.Success() : ApiResponse.Fail();
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<ApiResponse>> Put([FromBody] WorkItem workItem)
        {
            ValidateResult validateResult = await _service.UpdateAsync(workItem);
            return validateResult.Code > 0 ? ApiResponse.Success() : ApiResponse.Fail();
        }
    }
}
