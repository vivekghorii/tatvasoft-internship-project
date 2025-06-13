using Mission.Entities;
using Mission.Entities.Models;
using Mission.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mission.Entity.Entities;

namespace Mission.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(ILoginService loginService, IWebHostEnvironment hostingEnvironment) : ControllerBase
    {
        private readonly ILoginService _loginService = loginService;
        private readonly IWebHostEnvironment _hostingEnvironment = hostingEnvironment;
        ResponseResult result = new ResponseResult();

        [HttpPost]
        [Route("LoginUser")]
        public ResponseResult LoginUser(LoginUserRequestModel model)
        {
            try
            {
                result.Data = _loginService.LoginUser(model);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserRequestModel registerUserRequest)
        {
            try
            {
                var res = await _loginService.RegisterUser(registerUserRequest);
                return Ok(new ResponseResult() { Data = "User registered", Result = ResponseStatus.Success, Message = "" });
            }
            catch
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "Failed to add user." });
            }
        }

        [HttpGet]
        [Route("LoginUserDetailById/{id}")]
        public ResponseResult LoginUserDetailById(int id)
        {
            try
            {
                result.Data = _loginService.LoginUserDetailById(id);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        [Route("LoginUserProfileUpdate")]
        public async Task<ActionResult> LoginUserProfileUpdate([FromBody] AddUserDetailsRequestModel requestModel)
        {
            try
            {
                var res = await _loginService.LoginUserProfileUpdate(requestModel);
                return Ok(new ResponseResult() { Data = "Data Updated!", Result = ResponseStatus.Success, Message = "" });
            }
            catch
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "Failed to add user." });
            }
        }

        [HttpGet]
        [Route("GetUserProfileDetailById/{userId}")]
        public ResponseResult GetUserProfileDetailById(int userId)
        {
            try
            {
                result.Data = _loginService.GetUserProfileDetailById(userId);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
