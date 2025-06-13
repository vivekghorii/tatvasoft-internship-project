using Microsoft.AspNetCore.Mvc;
using Mission.Entities;
using Mission.Entities.Models;
using Mission.Entity.Entities;
namespace Mission.Services.IServices
{
    public interface ILoginService
    {
        ResponseResult LoginUser(LoginUserRequestModel model);

        LoginUserResponseModel UserLogin(LoginUserRequestModel model);

        Task<string> RegisterUser(RegisterUserRequestModel registerUserRequest);
        UserResponseModel LoginUserDetailById(int id);
        Task<bool> LoginUserProfileUpdate(AddUserDetailsRequestModel requestModel);
        UserDetail GetUserProfileDetailById(int userId);

    }
}
