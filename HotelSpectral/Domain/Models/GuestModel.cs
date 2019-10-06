using System;
using System.ComponentModel.DataAnnotations;

namespace HotelSpectral.Domain.Models
{
    public class GuestModel : BaseModel
    {
       
        public object GuestNo { get; set; }
        public bool Status { get; set; }
        public int? UserId { get; set; }

        public GuestModel()
        {
            GuestNo = Guid.NewGuid().ToString();
        }
    }
}
