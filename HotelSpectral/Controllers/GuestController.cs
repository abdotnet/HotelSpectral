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
    [Route("api/v1/[controller]")]
    public class GuestController : ApiController
    {
        private readonly IGuestService _iGuestService;

        public GuestController(IGuestService iGuestService)
        {
            _iGuestService = iGuestService;
        }

        // GET: api/values
        [HttpPost("guest")]
        public async Task<IActionResult> AddGuest([FromBody] GuestModel model)
        {
            ApiResponse reponse = await _iGuestService.AddGuestAsync(model);

            return Ok(reponse);
        }

        // GET: api/values
        [HttpGet("guest/{pageIndex}/{pageSize}")]
        public async Task<IActionResult> GetGuests(int pageIndex, int pageSize)
        {
            ApiResponse reponse = await _iGuestService.GetGuestAsync(pageIndex, pageSize);

            return Ok(reponse);
        }
    }
}