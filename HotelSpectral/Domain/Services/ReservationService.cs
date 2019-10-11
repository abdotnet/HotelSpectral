using System;
using System.Threading.Tasks;
using HotelSpectral.Data;
using HotelSpectral.Data.Entities;
using HotelSpectral.Domain.Enum;
using HotelSpectral.Domain.Interfaces;
using HotelSpectral.Domain.Models;
using System.Linq;
using HotelSpectral.Domain.Infrastructure;

namespace HotelSpectral.Domain.Services
{
    public class ReservationService : IReservationService
    {
        private HotelSpectralContext _context;

        public ReservationService(HotelSpectralContext context)
        {
            _context = context;

        }

        ///Add Guest 
        public async Task<ApiResponse> AddReservationAsync(ReservationModel model)
        {
            ApiResponse response = new ApiResponse();

            if (model == null) throw new Exception("Reservation data cannot be empty");

            Reservation reservation = new Reservation()
            {

                Status = true,
                NoOfNights = model.NoOfNights,
                Adult = model.Adult,
                Breakfast = model.Breakfast,
                CheckInDate = model.CheckInDate,
                CheckoutDate = model.CheckoutDate,
                Children = model.Children,
                Comments = model.Comments,
                CreatedDate = DateTime.Now,
                GuestId = model.GuestId,
                ReservationDate = model.ReservationDate,
                RoomId = model.RoomId
            };

            _context.Add<Reservation>(reservation);

            int result = await _context.SaveChangesAsync();

            if (result <= 0)
                throw new Exception("Database error");

            response.ResponseCode = Responses.SUCCESS_CODE;
            response.ResponseMessage = Responses.SUCCESS_MESSAGE;
            response.ResponseData = reservation;

            return response;

        }

        public async Task<ApiResponse> GetReservationAsync(int pageIndex, int pageSize)
        {
            ApiResponse response = new ApiResponse();

            return await Task.Run<ApiResponse>(() =>
            {
                String guestNo = Guid.NewGuid().ToString().Split('-')[0].ToString().ToUpper();

                var reservations = _context.Reservations.Select(c => new ReservationModel
                {
                    RoomId = c.RoomId,
                    Adult = c.Adult,
                    Breakfast = c.Breakfast,
                    CheckInDate = c.CheckInDate,
                    CheckoutDate = c.CheckoutDate,
                    Children = c.Children,
                    Comments = c.Comments,
                    CreatedDate = c.CreatedDate,
                    GuestId = c.GuestId,
                    NoOfNights = c.NoOfNights,
                    ReservationDate = c.ReservationDate,
                    Status = c.Status,
                    Id = c.Id,
                });

                long totalCount = reservations.LongCount();

                if (pageIndex > 0) reservations = reservations.OrderByDescending(c => c.Id).Skip(pageIndex * pageSize).Take(pageSize);

                var query = reservations.OrderByDescending(c => c.Id).Skip(pageIndex * pageSize).Take(pageSize);

                response.ResponseCode = Responses.SUCCESS_CODE;
                response.ResponseMessage = Responses.SUCCESS_MESSAGE;
                response.ResponseData = new Pagination<ReservationModel>(query.ToArray(), totalCount, pageSize, pageIndex);

                return response;

            });






        }

    }
}
