using EPM.IService.Service;
using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Dto.Response.DeptResponse;
using EPM.Model.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        private readonly IDepartmentService _service;

        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("getlist")]
        public async Task<ActionResult<ApiResponseWithData<List<DeptResponseDto>>>> Getlist()
        {
            ApiResponseWithData<List<DeptResponseDto>> result = new ApiResponseWithData<List<DeptResponseDto>>().Success();

            var responseDto = await _service.GetList();
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
        /// 新增部门
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Post([FromBody] Department department)
        {
            ValidateResult validateResult = await _service.AddAsync(department);
            return validateResult.Code > 0 ? ApiResponse.Success() : ApiResponse.Fail();
        }

        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<ApiResponse>> Put([FromBody] Department department)
        {
            ValidateResult validateResult = await _service.UpdateAsync(department);
            return validateResult.Code > 0 ? ApiResponse.Success() : ApiResponse.Fail();
        }


        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            ValidateResult validateResult = await _service.DeleteAsync(id);
            return validateResult.Code > 0 ? ApiResponse.Success() : ApiResponse.Fail();
        }

        /// <summary>
        /// 根据当前用户获取授权的部门
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDept")]
        public async Task<ActionResult<ApiResponseWithData<List<DeptTest>>>> GetDept(Guid id)
        {
            ApiResponseWithData<List<DeptTest>> result = new ApiResponseWithData<List<DeptTest>>().Success();

            //var responseDto = await _service.GetList();
            //if (responseDto != null)
            //{
            //    result.Data = responseDto.ToList();
            //    result.Count = responseDto.Count();
            //}
            //else
            //{
            //    result = result.Fail();
            //}

            //List<DeptTest> list = new List<DeptTest>()
            //{
            //    new DeptTest()
            //    {
            //        label="技术中心",
            //        value=Guid.NewGuid().ToString()
            //    },
            //    new DeptTest()
            //    {
            //        label="行政部",
            //        value=Guid.NewGuid().ToString()
            //    }
            //};

            List<DeptTest> list = await Task.Run(() => 
            {
                return new List<DeptTest>()
                {
                        new DeptTest()
                        {
                            label="技术中心",
                            value=Guid.NewGuid().ToString()
                        },
                        new DeptTest()
                        {
                            label="行政部",
                            value=Guid.NewGuid().ToString()
                        }
                };
            
            });

            result.Data = list;
            return result;
        }
    }

    public class DeptTest
    {
        public string label { get; set; }

        public string value { get; set; }
    }
}
