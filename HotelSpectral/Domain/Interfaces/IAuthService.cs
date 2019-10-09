using System;
using System.Threading.Tasks;
using HotelSpectral.Domain.Models;

namespace HotelSpectral.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse> AddRole(String roleName);
    }
}
