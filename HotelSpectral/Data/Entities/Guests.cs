using System;
using System.ComponentModel.DataAnnotations;

namespace HotelSpectral.Data.Entities
{
    public class Guests : BaseEntity 
    {
        public object GuestNo { get; set; }
        public Enum Status { get; set; }
        public int? UserId { get; set; }

       
    }
}
