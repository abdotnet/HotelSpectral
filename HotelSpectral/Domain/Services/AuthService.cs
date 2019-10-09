using System;
using HotelSpectral.Data;
using HotelSpectral.Domain.Interfaces;
using System.Linq;
using System.Collections.Generic;
using HotelSpectral.Data.Entities;
using System.Threading.Tasks;
using HotelSpectral.Domain.Models;

namespace HotelSpectral.Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly HotelSpectralContext _context;

        public AuthService(HotelSpectralContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse> AddRole(String roleName)
        {
            ApiResponse apiResponse = new ApiResponse();

            // string[] roles = { "Administrator", "Staff", "Guest" };

            bool exist = _context.Roles.Any(c => c.RoleName.ToUpper() == roleName.ToUpper());

            if (exist) throw new Exception("Role name already exist");


            var role = new Role()
            {
                CreatedDate = DateTime.Now,
                RoleName = roleName,
                Status = true
            };

            await _context.Roles.AddAsync(role);
            int result = await _context.SaveChangesAsync();

            if (result <= 0) throw new Exception("Database error");

            apiResponse.ResponseCode = Responses.SUCCESS_CODE;
            apiResponse.ResponseMessage = Responses.SUCCESS_MESSAGE;
            apiResponse.ResponseData = role;

            return apiResponse;
        }
    }
}
