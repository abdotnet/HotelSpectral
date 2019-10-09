using System;
using System.ComponentModel.DataAnnotations;

namespace HotelSpectral.Data.Entities
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public String  TransactionNo { get; set; }
        public String  ReceiptNo { get; set; }
        public int ReservationId { get; set; }
        public int GuestId { get; set; }
        public DateTime PaymentDate { get; set; } = new DateTime();
        public decimal Amount { get; set; }
        public int PaymentStatus { get; set; }
        public int CreatedBy { get; set; }
    }
}
