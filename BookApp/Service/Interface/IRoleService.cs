using BookApp.Models;
using BookApp.Models.Role;

namespace BookApp.Service.Interface;

public interface IRoleService
{
    Task<BaseResponseModel> CreateRole(CreateRoleViewModel request);
    Task<BaseResponseModel> DeleteRole(string roleId);
    Task<BaseResponseModel> UpdateRole(string roleId, UpdateRoleViewModel request);
    Task<RoleResponseModel> GetRole(string roleId);
    Task<RolesResponseModel> GetAllRole();
}
