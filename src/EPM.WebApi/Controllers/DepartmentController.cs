using EPM.Model.ApiModel;
using EPM.Model.DbModel;
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
    public class DepartmentController : ControllerBase
    {


        /// <summary>
        /// 分页获取所有用户信息
        /// </summary>
        /// <param name="searchRequestDto"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getlist")]
        public async Task<ActionResult<ApiResponseWithData<List<Department>>>> Getlist()
        {
            ApiResponseWithData<List<Department>> result = new ApiResponseWithData<List<Department>>();

            //var responseDto = await _userService.GetPatgeListAsync(pagingRequest);
            //if (responseDto != null)
            //{
            //    result.Data = responseDto.ResponseData;
            //    result.Count = responseDto.Count;
            //    return result.Success();
            //}
            //else
            //{
            //    return result.Fail();
            //}



            List<Department> list = new List<Department>()
            {
                new Department()
                {
                    Name="技术中心",
                    IsDeleted=0,
                    CreateUser="admin",
                    UpdateUser="admin"
                }
            };

            result.Data = list;
            result.Count = list.Count;
            return result.Success();
        }
    }
}
