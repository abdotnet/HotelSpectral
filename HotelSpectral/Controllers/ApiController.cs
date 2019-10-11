using System.Collections.Generic;
using System.Net;
using HotelSpectral.Domain.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelSpectral.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ValidateModel]
    [Route("api/v1/[controller]")]
    public class ApiController : Controller
    {

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
