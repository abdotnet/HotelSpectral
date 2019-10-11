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
    public class PaymentController : ApiController
    {
        private readonly IPaymentService _iPaymentService;

        public PaymentController(IPaymentService iPaymentService)
        {
            _iPaymentService = iPaymentService;
        }

        // GET: api/values
        [HttpPost("payment")]
        public async Task<IActionResult> AddReservation([FromBody] PaymentModel model)
        {
            ApiResponse reponse = await _iPaymentService.AddPaymentAsync(model);

            return Ok(reponse);
        }

        // GET: api/values
        [HttpGet("payment/{pageIndex}/{pageSize}")]
        public async Task<IActionResult> GetReservations(int pageIndex, int pageSize)
        {
            ApiResponse reponse = await _iPaymentService.GetPaymentAsync(pageIndex, pageSize);

            return Ok(reponse);
        }




    }
}
