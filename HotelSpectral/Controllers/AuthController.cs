using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelSpectral.Domain.Enum;
using HotelSpectral.Domain.Interfaces;
using HotelSpectral.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelSpectral.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class AuthController : ApiController
    {
        private readonly IAuthService _iAuthService;
        private readonly IConfiguration _config;

        public AuthController(IAuthService iAuthService, IConfiguration config)
        {
            _iAuthService = iAuthService;
            _config = config;
        }

        #region Manage Roles
        // GET: api/values
        [HttpPost("role")]
        public async Task<IActionResult> AddRole([FromBody] RoleModel model)
        {
            ApiResponse reponse = await _iAuthService.AddRoleAsync(model);

            return Ok(reponse);
        }

        // GET: api/values
        [HttpGet("role/{pageIndex}/{pageSize}")]
        public async Task<IActionResult> GetRoles(int pageIndex, int pageSize)
        {
            ApiResponse reponse = await _iAuthService.GetRolesAsync(pageIndex, pageSize);

            return Ok(reponse);
        }
        [HttpGet("role/{roleId}")]
        public async Task<IActionResult> GetRoles(int roleId)
        {
            ApiResponse reponse = await _iAuthService.GetRoleByIdAsync(roleId);

            return Ok(reponse);
        }
        #endregion

        #region Manage Users

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> AddGuestUser([FromBody]UserReqModel model)
        {

            ApiResponse reponse = await _iAuthService.AddUserAsync(model, (int)UserRole.Guest);

            return Ok(reponse);
        }

        // For admin and staffs  ...
        [HttpPost("user")]
        public async Task<IActionResult> AddUser([FromBody]UserAdminModel model)
        {

            ApiResponse reponse = await _iAuthService.AddUserAsync(model, model.RoleId);

            return Ok(reponse);
        }

        [HttpGet("user/{pageIndex}/{pageSize}")]
        public async Task<IActionResult> GetUsers(int pageIndex, int pageSize)
        {
            ApiResponse reponse = await _iAuthService.GetUsersAsync(pageIndex, pageSize);
            return Ok(reponse);
        }
        [HttpPost("user/{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            ApiResponse reponse = await _iAuthService.GetUserByIdAsync(userId);
            return Ok(reponse);
        }
        [HttpGet("user/search/{userName}")]
        public async Task<IActionResult> GetUsers([Required]string userName)
        {
            ApiResponse reponse = await _iAuthService.GetUserByNameAsync(userName);

            return Ok(reponse);
        }

        #endregion

        #region validation user & generate token


        [HttpPost("token")]
        [AllowAnonymous]
        public async Task<IActionResult> GeToken([FromBody]LoginModel model)
        {

            ApiResponse reponse = await UserLogin(model);

            return Ok(reponse);
        }

        private async Task<ApiResponse> UserLogin(LoginModel model)
        {
            ApiResponse response = new ApiResponse();


            var _user = await _iAuthService.ValidateUser(model.Username, model.Password);

            if (_user == null || _user.ResponseData == null) throw new Exception("User information not found");


            var user = (TokenModel)_user.ResponseData;

            var claims = new[]
            {
                            new System.Security.Claims.Claim(JwtRegisteredClaimNames.Sub,user.Email),
                            new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                            new System.Security.Claims.Claim(JwtRegisteredClaimNames.UniqueName,user.Email),
                            new System.Security.Claims.Claim("UserId",user.UserId.ToString()),
                            new System.Security.Claims.Claim("FirstName",user.Firstname),
                            new System.Security.Claims.Claim("LastName",user.LastName),
                             new System.Security.Claims.Claim("Role",user.RolId.ToString()),
                             new System.Security.Claims.Claim("LastLoginDate",user.LastLoginDate.ToString()),
                        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));


            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Tokens:Issuer"],
                _config["Tokens:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(1000000),
                signingCredentials: creds
                );

            var _token = new TokenResponseModel()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                User = user
            };




            response.ResponseCode = Responses.SUCCESS_CODE;
            response.ResponseMessage = Responses.SUCCESS_MESSAGE;
            response.ResponseData = _token;

            return response;
        }

        #endregion

    }
}







