using System;
using System.Threading.Tasks;
using HotelSpectral.Data;
using HotelSpectral.Data.Entities;
using HotelSpectral.Domain.Enum;
using HotelSpectral.Domain.Models;

namespace HotelSpectral.Domain.Services
{
    public class GuestService
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
        public async Task<ApiResponseModel> AddGuestAsync(GuestModel model)
        {
            ApiResponseModel response = new ApiResponseModel();

            if (model == null) throw new Exception("Guest data cannot be empty");

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
                GuestNo = model.GuestNo,
                LastName = model.LastName,
                Mobile = model.Mobile,
                Religion = model.Religion,
                Title = model.Title,
                UserId = model.UserId,
                NationlID = model.NationlID,
                NationalIDNo = model.NationalIDNo,
                Status = AppStatus.InActive
            };

            _context.Add<Guests>(guest);

            int result = await _context.SaveChangesAsync();

            if (result <= 0)
            {
                throw new Exception("Database error");
            }

            response.ResponseCode = AppResponseModel.SUCCESS_CODE;
            response.ResponseMessage = AppResponseModel.SUCCESS_MESSAGE;

            return response;

        }
    }
}
