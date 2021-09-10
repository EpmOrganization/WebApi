using EPM.Framework.Helper;
using EPM.IService.Service;
using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Dto.Request.DataAuthority;
using EPM.Model.Dto.Response.DataAuthorityResponse;
using EPM.Service.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.WebApi.Controllers
{
    /// <summary>
    /// 数据权限分配
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DataAuthorityController : ControllerBase
    {
        private readonly IDataAuthorityService _service;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public DataAuthorityController(IDataAuthorityService service)
        {
            _service = service;
        }


        /// <summary>
        /// 根据用户id查询数据权限
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponseWithData<List<DataAuthorityResponseDto>>>> Get(Guid id)
        {
            ApiResponseWithData<List<DataAuthorityResponseDto>> result = new ApiResponseWithData<List<DataAuthorityResponseDto>>().Success();
            var data = await _service.GetListDtoAsync(id);
            result.Data = data.ToList();
            return result;
        }


        /// <summary>
        /// 设置用户权限
        /// </summary>
        /// <param name="authorityAllot"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Post([FromBody] DataAuthorityRequestDto authorityAllot)
        {
            ValidateResult validateResult = await _service.AddAsync(authorityAllot);
            return validateResult.Code > 0 ? ApiResponse.Success() : ApiResponse.Fail();
        }
    }
}
