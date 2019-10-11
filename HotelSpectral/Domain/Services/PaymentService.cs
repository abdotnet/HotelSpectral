using System;
using System.Threading.Tasks;
using HotelSpectral.Data;
using HotelSpectral.Data.Entities;
using HotelSpectral.Domain.Interfaces;
using HotelSpectral.Domain.Models;
using System.Linq;
using HotelSpectral.Domain.Infrastructure;

namespace HotelSpectral.Domain.Services
{
    public class PaymentService : IPaymentService
    {
        private HotelSpectralContext _context;
        public PaymentService(HotelSpectralContext context)
        {
            _context = context;

        }

        //// Add guest
        ///remove guest
        ///update guest
        ///select all guest
        ///get guest by guest Id


        ///Add Guest 
        public async Task<ApiResponse> AddPaymentAsync(PaymentModel model)
        {
            ApiResponse response = new ApiResponse();

            if (model == null) throw new Exception("Payment data cannot be empty");

            String transactionNo = Guid.NewGuid().ToString().Split('-')[4].ToUpper();
            Payment payment = new Payment()
            {
                Amount = model.Amount,
                CreatedBy = model.CreatedBy,
                GuestId = model.GuestId,
                PaymentDate = DateTime.Now,
                PaymentStatus = model.PaymentStatus,
                ReceiptNo = model.ReceiptNo,
                ReservationId = model.ReservationId,
                TransactionNo = transactionNo,
            };

            _context.Add<Payment>(payment);

            int result = await _context.SaveChangesAsync();

            if (result <= 0)
                throw new Exception("Database error");

            response.ResponseCode = Responses.SUCCESS_CODE;
            response.ResponseMessage = Responses.SUCCESS_MESSAGE;
            response.ResponseData = payment;

            return response;

        }

        public async Task<ApiResponse> GetPaymentAsync(int pageIndex, int pageSize)
        {
            ApiResponse response = new ApiResponse();

            return await Task.Run<ApiResponse>(() =>
            {
                String guestNo = Guid.NewGuid().ToString().Split('-')[0].ToString().ToUpper();

                var guests = _context.Payments.Select(c => new PaymentModel
                {
                    Amount = c.Amount,
                    CreatedBy = c.CreatedBy,
                    GuestId = c.GuestId,
                    PaymentDate = c.PaymentDate,
                    PaymentStatus = c.PaymentStatus,
                    ReceiptNo = c.ReceiptNo,
                    ReservationId = c.ReservationId,
                    TransactionNo = c.TransactionNo,
                    Id = c.Id,
                });

                long totalCount = guests.LongCount();

                if (pageIndex > 0) guests = guests.OrderByDescending(c => c.Id).Skip(pageIndex * pageSize).Take(pageSize);

                var query = guests.OrderByDescending(c => c.Id).Skip(pageIndex * pageSize).Take(pageSize);

                response.ResponseCode = Responses.SUCCESS_CODE;
                response.ResponseMessage = Responses.SUCCESS_MESSAGE;
                response.ResponseData = new Pagination<PaymentModel>(query.ToArray(), totalCount, pageSize, pageIndex);

                return response;

            });






        }
    }
}
