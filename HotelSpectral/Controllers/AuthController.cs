using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelSpectral.Domain.Interfaces;
using HotelSpectral.Domain.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelSpectral.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class AuthController : ApiController
    {
        private readonly IAuthService _iAuthService;

        public AuthController(IAuthService iAuthService)
        {
            _iAuthService = iAuthService;
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
            ApiResponse reponse = await _iAuthService.GetRolesAsync();

            return Ok(reponse);
        }
        [HttpGet("role/{roleId}")]
        public async Task<IActionResult> GetRoles(int roleId)
        {
            ApiResponse reponse = await _iAuthService.GetRolesAsync();

            return Ok(reponse);
        }
        #endregion

        #region Manage Users

        [HttpPost("user")]
        public async Task<IActionResult> AddUser()
        {
            return Ok(null);
        }
        [HttpGet("user/{pageIndex}/{pageSize}")]
        public async Task<IActionResult> GetUsers(int pageIndex, int pageSize)
        {
            
            return Ok(null);
        }

        #endregion
    }
}
