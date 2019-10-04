using System;
namespace HotelSpectral.Domain.Models
{
    public class ReservationModel
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int RoomId { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckoutDate { get; set; }
        public int Adult { get; set; }
        public int Children { get; set; }
        public bool Breakfast { get; set; }
        public String Comments { get; set; }
        public int NoOfNights { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; } = new DateTime();
    }
}
