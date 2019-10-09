using System;
namespace HotelSpectral.Domain.Models
{
    public class ApiResponse
    {
        public String ResponseCode { get; set; }
        public string  ResponseMessage { get; set; }
        public Object  ResponseData { get; set; }
    }
}
