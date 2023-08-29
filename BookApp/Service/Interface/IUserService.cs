using BookApp.Models;
using BookApp.Models.Auth;
using BookApp.Models.User;

namespace BookApp.Service.Interface;

public interface IUserService
{
    Task<UserResponseModel> GetUser(string userId);
    Task<BaseResponseModel> Register(SignUpViewModel request, string roleName = null);
    Task<UserResponseModel> Login(LoginViewModel request);
}
