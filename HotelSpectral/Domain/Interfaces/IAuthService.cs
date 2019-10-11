using System;
using System.Threading.Tasks;
using HotelSpectral.Domain.Models;

namespace HotelSpectral.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse> AddRoleAsync(RoleModel model);
        Task<ApiResponse> GetRolesAsync(int pageIndex, int pageSize);
        Task<ApiResponse> GetRoleByIdAsync(int roleId);
        Task<ApiResponse> AddUserAsync(UserReqModel model, int roleId);
        Task<ApiResponse> GetUsersAsync(int pageIndex, int pageSize);
        Task<ApiResponse> GetUserByIdAsync(int userId);
        Task<ApiResponse> GetUserByNameAsync(string userName);
        Task<ApiResponse> ValidateUser(string userName, string password);
    }
}
