using System;
namespace HotelSpectral.Domain.Models
{
    public class ApiResponseModel
    {
        public String ResponseCode { get; set; }
        public string  ResponseMessage { get; set; }
        public Object  ResponseData { get; set; }
    }
}
