using System;
using System.ComponentModel.DataAnnotations;

namespace HotelSpectral.Data.Entities
{
    public class Guests : BaseEntity 
    {
        public String GuestNo { get; set; }
        public int Status { get; set; }
        public int? UserId { get; set; }

       
    }
}
