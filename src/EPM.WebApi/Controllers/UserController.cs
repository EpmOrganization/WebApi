using EPM.IService.Service;
using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Dto.Response.UserResponse;
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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 分页获取所有用户信息
        /// </summary>
        /// <param name="searchRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getlist")]
        public async Task<ActionResult<ApiResponseWithData<List<UserDto>>>> Getlist([FromBody] PagingRequest pagingRequest)
        {
            ApiResponseWithData<List<UserDto>> result = new ApiResponseWithData<List<UserDto>>().Success();

            var responseDto =await _userService.GetPatgeListAsync(pagingRequest);
            if (responseDto != null)
            {
                result.Data = responseDto.ResponseData;
                result.Count = responseDto.Count;
            }
            else
            {
               result= result.Fail();
            }
            return result;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Post([FromBody] User user)
        {
            ValidateResult validateResult = await _userService.AddAsync(user);
            return validateResult.Code > 0 ? ApiResponse.Success() : ApiResponse.Fail();
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<ApiResponse>> Put([FromBody] User user)
        {
            ValidateResult validateResult = await _userService.UpdateAsync(user);
            return validateResult.Code > 0 ? ApiResponse.Success() : ApiResponse.Fail();
        }


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            ValidateResult validateResult = await _userService.DeleteAsync(id);
            return validateResult.Code > 0 ? ApiResponse.Success() : ApiResponse.Fail();
        }

        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCurrentUser")]
        public async Task<ActionResult<ApiResponseWithData<User>>> GetCurrentUser()
        {
            ApiResponseWithData<User> result = new ApiResponseWithData<User>().Success();
            // User currentUser = await _tokenService.GetUserByToken();
            //LoginInfo loginInfo = await _tokenService.GetLoginInfoByToken();
            //result.ResultCode = ResultCode.Success;
            //result.ResultMsg = EnumHelper.GetEnumDesc(ResultCode.Success);

            LoginInfo loginInfo = new LoginInfo()
            {
                LoginUser = new User()
                {
                    Name="测试"
                }
            };
            result.Data = loginInfo.LoginUser;
            return result;
        }
    }
}
