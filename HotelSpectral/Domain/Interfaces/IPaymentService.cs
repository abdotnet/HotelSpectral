using System;
using System.Threading.Tasks;
using HotelSpectral.Domain.Models;

namespace HotelSpectral.Domain.Interfaces
{
    public interface IPaymentService
    {
        Task<ApiResponse> AddPaymentAsync(PaymentModel model);
        Task<ApiResponse> GetPaymentAsync(int pageIndex, int pageSize);
    }
}
