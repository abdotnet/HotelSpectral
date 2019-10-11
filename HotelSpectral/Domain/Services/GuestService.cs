using System;
using System.Threading.Tasks;
using HotelSpectral.Data;
using HotelSpectral.Data.Entities;
using HotelSpectral.Domain.Enum;
using HotelSpectral.Domain.Infrastructure;
using HotelSpectral.Domain.Interfaces;
using HotelSpectral.Domain.Models;
using System.Linq;

namespace HotelSpectral.Domain.Services
{
    public class GuestService : IGuestService
    {
        private HotelSpectralContext _context;
        public GuestService(HotelSpectralContext context)
        {
            _context = context;

        }

        //// Add guest
        ///remove guest
        ///update guest
        ///select all guest
        ///get guest by guest Id


        ///Add Guest 
        public async Task<ApiResponse> AddGuestAsync(GuestModel model)
        {
            ApiResponse response = new ApiResponse();

            if (model == null) throw new Exception("Guest data cannot be empty");

            String guestNo = Guid.NewGuid().ToString().Split('-')[0].ToUpper();

            Guests guest = new Guests()
            {
                Address = model.Address,
                City = model.City,
                Country = model.Country,
                CreatedDate = new DateTime(),
                DOB = model.DOB,
                EmailAddress = model.EmailAddress,
                FirstName = model.FirstName,
                Gender = model.Gender,
                GuestNo = guestNo,
                LastName = model.LastName,
                Mobile = model.Mobile,
                Religion = model.Religion,
                Title = model.Title,
                UserId = model.UserId,
                NationlID = model.NationlID,
                NationalIDNo = model.NationalIDNo,
                Status = (int)AppStatus.Active
            };

            _context.Add<Guests>(guest);

            int result = await _context.SaveChangesAsync();

            if (result <= 0)
                throw new Exception("Database error");

            response.ResponseCode = Responses.SUCCESS_CODE;
            response.ResponseMessage = Responses.SUCCESS_MESSAGE;
            response.ResponseData = guest;

            return response;

        }

        public async Task<ApiResponse> GetGuestAsync(int pageIndex, int pageSize)
        {
            ApiResponse response = new ApiResponse();

            return await Task.Run<ApiResponse>(() =>
          {
              String guestNo = Guid.NewGuid().ToString().Split('-')[0].ToString().ToUpper();

              var guests = _context.Guests.Select(c => new GuestModel
              {
                  Address = c.Address,
                  City = c.City,
                  Country = c.Country,
                  CreatedDate = c.CreatedDate,
                  DOB = c.DOB,
                  EmailAddress = c.EmailAddress,
                  FirstName = c.FirstName,
                  Gender = c.Gender,
                  LastName = c.LastName,
                  GuestNo = guestNo,
                  Id = c.Id,
              });

              long totalCount = guests.LongCount();

              if (pageIndex > 0) guests = guests.OrderByDescending(c => c.Id).Skip(pageIndex * pageSize).Take(pageSize);

              var query = guests.OrderByDescending(c => c.Id).Skip(pageIndex * pageSize).Take(pageSize);

              response.ResponseCode = Responses.SUCCESS_CODE;
              response.ResponseMessage = Responses.SUCCESS_MESSAGE;
              response.ResponseData = new Pagination<GuestModel>(query.ToArray(), totalCount, pageSize, pageIndex);

              return response;

          });






        }
    }
}
