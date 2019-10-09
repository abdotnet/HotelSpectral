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

        // GET: api/values
        [HttpPost("role")]
        public async Task<IActionResult> AddRole([FromBody] String roleName)
        {
            ApiResponse reponse = await _iAuthService.AddRole(roleName);

            return Ok(reponse);
        }

      
    }
}
