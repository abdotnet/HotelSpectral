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
        public String OtherNames { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        public int Gender { get; set; }
        public int Title { get; set; }
        [Required]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(16, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 5)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
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
