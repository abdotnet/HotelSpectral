using System;
using System.ComponentModel.DataAnnotations;

namespace HotelSpectral.Data.Entities
{
    public class AuditTrail
    {
        [Key]
        public int Id { get; set; }
        public String  Action { get; set; }
        public String  IPAddress { get; set; }
        public int  UserId { get; set; }
        public String  TableAffected { get; set; }
        public int  RowAffected { get; set; }
        public String  DataChanged { get; set; }
        public DateTime AuditDate { get; set; } = new DateTime();
    }
}
