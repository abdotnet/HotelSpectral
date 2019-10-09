using System;
using System.ComponentModel.DataAnnotations;

namespace HotelSpectral.Domain.Models
{
    public class GuestModel : BaseModel
    {
       
        public String GuestNo { get; set; }
        public bool Status { get; set; }
        public int? UserId { get; set; }

        public GuestModel()
        {
            GuestNo = Guid.NewGuid().ToString();
        }
    }
}
