using System;
namespace HotelSpectral.Domain.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public DateTime DOB { get; set; }
        public int Gender { get; set; }
        public int Title { get; set; }
        public string Mobile { get; set; }
        public string EmailAddress { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String Country { get; set; }
        public int Religion { get; set; }
        public int NationlID { get; set; }
        public String NationalIDNo { get; set; }
        public DateTime CreatedDate { get; set; } = new DateTime();
    }
}
