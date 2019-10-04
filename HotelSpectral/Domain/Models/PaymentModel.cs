using System;
namespace HotelSpectral.Domain.Models
{
    public class PaymentModel
    {
        public int Id { get; set; }
        public String TransactionNo { get; set; }
        public String ReceiptNo { get; set; }
        public int ReservationId { get; set; }
        public int GuestId { get; set; }
        public DateTime PaymentDate { get; set; } = new DateTime();
        public decimal Amount { get; set; }
        public int PaymentStatus { get; set; }
        public int CreatedBy { get; set; }
    }
}
