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
    public class ReservationController : ApiController
    {
        private readonly IReservationService _iReservationService;

        public ReservationController(IReservationService iReservationService)
        {
            _iReservationService = iReservationService;
        }

        // GET: api/values
        [HttpPost("reservation")]
        public async Task<IActionResult> AddReservation([FromBody] ReservationModel model)
        {
            ApiResponse reponse = await _iReservationService.AddReservationAsync(model);

            return Ok(reponse);
        }

        // GET: api/values
        [HttpGet("reservation/{pageIndex}/{pageSize}")]
        public async Task<IActionResult> GetReservations(int pageIndex, int pageSize)
        {
            ApiResponse reponse = await _iReservationService.GetReservationAsync(pageIndex, pageSize);

            return Ok(reponse);
        }


    }
}
