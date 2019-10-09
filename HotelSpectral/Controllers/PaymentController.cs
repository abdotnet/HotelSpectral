using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelSpectral.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class PaymentController : ApiController
    {
        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

      
    }
}
