using System;
using HotelSpectral.Data;
using HotelSpectral.Domain.Interfaces;
using System.Linq;
using System.Collections.Generic;
using HotelSpectral.Data.Entities;
using System.Threading.Tasks;
using HotelSpectral.Domain.Models;
using HotelSpectral.Domain.Infrastructure;

namespace HotelSpectral.Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly HotelSpectralContext _context;

        public AuthService(HotelSpectralContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse> AddRoleAsync(RoleModel model)
        {
            ApiResponse apiResponse = new ApiResponse();

            // string[] roles = { "Administrator", "Staff", "Guest" };

            if (model == null || String.IsNullOrEmpty(model.RoleName)) throw new Exception("Role name cannot be empty");

            bool exist = _context.Roles.Any(c => c.RoleName.ToUpper() == model.RoleName.ToUpper());

            if (exist) throw new Exception("Role name already exist");


            var role = new Role()
            {
                CreatedDate = DateTime.Now,
                RoleName = model.RoleName,
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

        public async Task<ApiResponse> GetRolesAsync(int pageIndex, int pageSize)
        {

            return await Task.Run<ApiResponse>(() =>
            {
                ApiResponse response = new ApiResponse();

                var roles = _context.Roles.Where(c => c.Status).Select(c => new RoleModel()
                {
                    Status = c.Status,
                    CreatedDate = c.CreatedDate,
                    Id = c.Id,
                    RoleName = c.RoleName
                });

                long totalCount = roles.LongCount();

                if (pageIndex > 0) roles = roles.OrderByDescending(c => c.Id).Skip(pageIndex * pageSize).Take(pageSize);

                var query = roles.OrderByDescending(c => c.Id).Skip(pageIndex * pageSize).Take(pageSize);

                response.ResponseCode = Responses.SUCCESS_CODE;
                response.ResponseMessage = Responses.SUCCESS_MESSAGE;
                response.ResponseData = new Pagination<RoleModel>(query.ToArray(), totalCount, pageSize, pageIndex);

                return response;
            });
        }

        public async Task<ApiResponse> GetRoleByIdAsync(int roleId)
        {

            return await Task.Run<ApiResponse>(() =>
            {
                ApiResponse response = new ApiResponse();

                var roles = _context.Roles.Where(c => c.Status && c.Id == roleId).Select(c => new RoleModel()
                {
                    Status = c.Status,
                    CreatedDate = c.CreatedDate,
                    Id = c.Id,
                    RoleName = c.RoleName
                }).ToList();

                response.ResponseCode = Responses.SUCCESS_CODE;
                response.ResponseMessage = Responses.SUCCESS_MESSAGE;
                response.ResponseData = roles;

                return response;
            });
        }

        public async Task<ApiResponse> AddUserAsync(UserReqModel model, int roleId)
        {
            ApiResponse apiResponse = new ApiResponse();

            if (model == null) throw new Exception("User cannot be empty");

            if (roleId <= 0) throw new Exception("Role must be selected");

            bool exist = _context.Users.Any(c => c.EmailAddress.ToUpper() == model.EmailAddress.ToUpper()
            || c.Mobile.ToUpper() == model.Mobile.ToUpper() || c.Username ==  model.Username);

            if (exist) throw new Exception("User name already exist");

            String salt = Guid.NewGuid().ToString();

            model.Password = UtilityService.EncryptPassword(model.Password, salt);

            DateTime dob = DateTime.Parse(model.DOB);

            var user = new User()
            {
                CreatedDate = DateTime.Now,
                LastLoginDate = DateTime.Now,
                EmailAddress = model.EmailAddress,
                Address = model.Address,
                City = model.City,
                Country = model.Country,
                DOB = dob,
                FirstName = model.FirstName,
                Gender = model.Gender,
                LastName = model.LastName,
                Mobile = model.Mobile,
                NationalIDNo = model.NationalIDNo,
                NationlID = model.NationlID,
                PictureName = model.PictureName,
                Password = model.Password,
                Salt = salt,
                Religion = model.Religion,
                Title = model.Title,
                Username = model.Username,
                UserType = model.UserType,
                Status = true,
            };

            await _context.Users.AddAsync(user);

            int result = await _context.SaveChangesAsync();

            var userRole = new UserRole();

            userRole.CreatedDate = DateTime.Now;
            userRole.RoleId = roleId;
            userRole.UserId = user.Id;

            await _context.UserRoles.AddAsync(userRole);

            result += await _context.SaveChangesAsync();

            if (result <= 1) throw new Exception("Database error");

            apiResponse.ResponseCode = Responses.SUCCESS_CODE;
            apiResponse.ResponseMessage = Responses.SUCCESS_MESSAGE;
            user.Password = null;
            user.Salt = null;
            apiResponse.ResponseData = user;

            return apiResponse;
        }

        public async Task<ApiResponse> GetUsersAsync(int pageIndex, int pageSize)
        {

            return await Task.Run<ApiResponse>(() =>
            {
                ApiResponse response = new ApiResponse();

                var users = from u in _context.Users
                            join ur in _context.UserRoles on u.Id equals ur.UserId
                            where u.Status && ur.Status
                            select new UserModel()
                            {
                                Status = u.Status,
                                CreatedDate = u.CreatedDate,
                                Id = u.Id,
                                FirstName = u.FirstName,
                                Address = u.Address,
                                City = u.City,
                                Country = u.Country,
                                DOB = u.DOB,
                                EmailAddress = u.EmailAddress,
                                Gender = u.Gender,
                                LastLoginDate = u.LastLoginDate,
                                LastName = u.LastName,
                                Mobile = u.Mobile,
                                NationalIDNo = u.NationalIDNo,
                                NationlID = u.NationlID,
                                PictureName = u.PictureName,
                                Religion = u.Religion,
                                Title = u.Title,
                                Username = u.Username,
                                UserType = u.UserType
                            };


                //if (!string.IsNullOrEmpty(search))
                //    query = query.Where(c => c.TransactionNo.Contains(search));

                long totalCount = users.LongCount();

                if (pageIndex > 0) users = users.OrderByDescending(c => c.Id).Skip(pageIndex * pageSize).Take(pageSize);

                var query = users.OrderByDescending(c => c.Id).Skip(pageIndex * pageSize).Take(pageSize);

                response.ResponseCode = Responses.SUCCESS_CODE;
                response.ResponseMessage = Responses.SUCCESS_MESSAGE;
                response.ResponseData = new Pagination<UserModel>(query.ToArray(), totalCount, pageSize, pageIndex);

                return response;
            });
        }

        public async Task<ApiResponse> GetUserByIdAsync(int userId)
        {

            return await Task.Run<ApiResponse>(() =>
            {
                ApiResponse response = new ApiResponse();


                var users = from u in _context.Users
                            join ur in _context.UserRoles on u.Id equals ur.UserId
                            where u.Status && ur.Status
                            where u.Id == userId
                            select new UserAdminModel()
                            {
                                //Status = u.Status,
                               // CreatedDate = u.CreatedDate,
                               // Id = u.Id,
                                FirstName = u.FirstName,
                                Address = u.Address,
                                City = u.City,
                                Country = u.Country,
                               // DOB = u.DOB,
                                EmailAddress = u.EmailAddress,
                                Gender = u.Gender,
                                //LastLoginDate = u.LastLoginDate,
                                LastName = u.LastName,
                                Mobile = u.Mobile,
                                NationalIDNo = u.NationalIDNo,
                                NationlID = u.NationlID,
                                Password = u.Password,
                                PictureName = u.PictureName,
                                Religion = u.Religion,
                                RoleId = ur.RoleId,
                                Title = u.Title,
                                Username = u.Username,
                                UserType = u.UserType
                            };


                response.ResponseCode = Responses.SUCCESS_CODE;
                response.ResponseMessage = Responses.SUCCESS_MESSAGE;
                response.ResponseData = users;

                return response;
            });
        }

        public async Task<ApiResponse> GetUserByNameAsync(string userName)
        {
            return await Task.Run<ApiResponse>(() =>
            {
                ApiResponse response = new ApiResponse();

                var query = from user in _context.Users
                            where user.Username == userName
                            where user.EmailAddress == userName
                            select user;

                var _user = query.FirstOrDefault();

                response.ResponseCode = Responses.SUCCESS_CODE;
                response.ResponseMessage = Responses.SUCCESS_MESSAGE;
                response.ResponseData = _user.Id;

                return response;
            });


        }


        public async Task<ApiResponse> ValidateUser(string userName, string password)
        {

            ApiResponse response = new ApiResponse();

            var _user = from u in _context.Users
                        join ur in _context.UserRoles on u.Id equals ur.UserId
                        where u.EmailAddress == userName || u.Username == userName
                        select new TokenInfoModel
                        {
                            Email = u.EmailAddress,
                            Firstname = u.FirstName,
                            LastLoginDate = u.LastLoginDate,
                            LastName = u.LastName,
                            PicturePath = u.PictureName,
                            UserId = u.Id,
                            RoleId = ur.RoleId,
                            IsActive = u.Status,
                            Salt = u.Salt,
                             Password = u.Password
                        };

            var user = _user.FirstOrDefault();

            if (user == null) throw new Exception("User not found");

            if (!user.IsActive) throw new Exception("User is not active");

            string pwd = UtilityService.EncryptPassword(password, user.Salt);

            if (!pwd.Equals(user.Password))
                throw new Exception("Either username or password is incorrect");

            var tokenModel = new TokenModel()
            {
                Email = user.Email,
                Firstname = user.Firstname,
                LastLoginDate = user.LastLoginDate,
                LastName = user.LastName,
                PicturePath = user.PicturePath,
                UserId = user.UserId,
                 RolId = user.RoleId
            };


            response.ResponseCode = Responses.SUCCESS_CODE;
            response.ResponseMessage = Responses.SUCCESS_MESSAGE;
            response.ResponseData = tokenModel;

            var user_ = _context.Users.FirstOrDefault(c => c.Id == tokenModel.UserId);

            // update last login date....
            user_.LastLoginDate = DateTime.Now;
            _context.Users.Update(user_);
            await _context.SaveChangesAsync();

            return response;
        }



    }

    // create permission , role permission ..


}
