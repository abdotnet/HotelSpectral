using System;
using System.ComponentModel.DataAnnotations;

namespace HotelSpectral.Domain.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
        [Required]
        public String FirstName { get; set; }
        [Required]
        public String LastName { get; set; }
        public String  OtherNames { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        public int Gender { get; set; }
        public int Title { get; set; }
        public string Mobile { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        [Required]
        public String Country { get; set; }
        public int Religion { get; set; }
        [Required]
        public int NationlID { get; set; }
        [Required]
        public String NationalIDNo { get; set; }
        public DateTime CreatedDate { get; set; } = new DateTime();
    }
}
