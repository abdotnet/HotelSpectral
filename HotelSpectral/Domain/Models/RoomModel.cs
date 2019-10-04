using System;
namespace HotelSpectral.Domain.Models
{
    public class RoomModel
    {
        public int Id { get; set; }
        public int RoomTypeId { get; set; }
        public String RoomNo { get; set; }
        public String Description { get; set; }
        public decimal RoomPrice { get; set; }
        public DateTime CreatedDate { get; set; } = new DateTime();
        public int MaxPerson { get; set; }
        public bool IsLocked { get; set; }
    }

    public class RoomTypeModel
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public DateTime CreatedDate { get; set; } = new DateTime();
        public bool Status { get; set; }
    }
}
