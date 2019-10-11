 using System;
using System.ComponentModel.DataAnnotations;

namespace HotelSpectral.Domain.Models
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public String Password { get; set; }
        public bool  RememberMe { get; set; }
    }
}
