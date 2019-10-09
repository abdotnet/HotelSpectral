using System;
namespace HotelSpectral.Domain.Models
{
    public class ApiResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string[] Errors { get; set; }
        public object ResponseData { get; set; }
       // public IEnumerable<KeyValuePair<string, string[]>> ValidationErrors { get; set; }
    }
}
