using System;
namespace HotelSpectral.Domain.Models
{
    public class TokenResponseModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public TokenModel User { get; set; }
    }
}
