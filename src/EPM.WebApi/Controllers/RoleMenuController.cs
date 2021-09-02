using EPM.IService.Service;
using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Dto.Response.RoleMenuResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EPM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleMenuController : ControllerBase
    {
        private readonly IRoleMenuService _service;

        public RoleMenuController(IRoleMenuService service)
        {
            _service = service;
        }

        /// <summary>
        /// 根据角色ID获取对应的权限明细
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponseWithData<RoleMenuResponseDto>>> Get(Guid id)
        {
            ApiResponseWithData<RoleMenuResponseDto> result = new ApiResponseWithData<RoleMenuResponseDto>().Success();
            RoleMenuResponseDto list = await _service.GetListAsync(p => p.ID == id);
            result.Data = list;
            return result;
        }
    }
}
