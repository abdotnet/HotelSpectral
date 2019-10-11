using System;
using System.Threading.Tasks;
using HotelSpectral.Domain.Models;

namespace HotelSpectral.Domain.Interfaces
{
    public interface IReservationService
    {
        Task<ApiResponse> AddReservationAsync(ReservationModel model);
        Task<ApiResponse> GetReservationAsync(int pageIndex, int pageSize);
    }
}
