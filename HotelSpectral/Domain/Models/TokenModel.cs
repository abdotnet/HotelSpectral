using System;
namespace HotelSpectral.Domain.Models
{
    public class TokenModel
    {
		public int UserId { get; set; }
        public String Email { get; set; }
        public String  Firstname { get; set; }
        public String  LastName { get; set; }
        public DateTime LastLoginDate { get; set; }
        public String  PicturePath { get; set; }
        public int  RolId { get; set; }



    }

    public class TokenInfoModel
    {
        public int UserId { get; set; }
        public String Email { get; set; }
        public String Firstname { get; set; }
        public String LastName { get; set; }
        public DateTime LastLoginDate { get; set; }
        public String PicturePath { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        public String  Salt { get; set; }
        public String  Password { get; set; }



    }
}
