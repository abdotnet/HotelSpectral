using System;
using System.Threading.Tasks;
using HotelSpectral.Domain.Models;

namespace HotelSpectral.Domain.Interfaces
{
    public interface IGuestService
    {
        Task<ApiResponse> AddGuestAsync(GuestModel model);
        Task<ApiResponse> GetGuestAsync(int pageIndex, int pageSize);
    }
}
