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
        Task<ApiResponse> AddUserAsync(UserModel model);
        Task<ApiResponse> GetUsersAsync(int pageIndex, int pageSize);
        Task<ApiResponse> GetUserByIdAsync(int userId);
    }
}
