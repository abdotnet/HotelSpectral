using System;
using System.ComponentModel.DataAnnotations;

namespace HotelSpectral.Data.Entities
{
    public class User : BaseEntity
    {
        public String Username { get; set; }
        public String Password { get; set; }
        public String Salt { get; set; }
        public int UserType { get; set; }
        public DateTime LastLoginDate { get; set; }
        public String PictureName { get; set; }
        public bool Status { get; set; }
    }

    public class Role
    {
        [Key]
        public int Id { get; set; }
        public String RoleName { get; set; }
        public DateTime CreatedDate { get; set; } = new DateTime();
        public bool Status { get; set; }
    }

    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime CreatedDate { get; set; } = new DateTime();
        public bool Status { get; set; }
    }

    public class Permission
    {
        [Key]
        public int Id { get; set; }
        public String Action { get; set; }
        public String Descrition { get; set; }
        public DateTime CreatedDate { get; set; } = new DateTime();
        public bool Status { get; set; }
    }
    public class RolePermission
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public DateTime CreatedDate { get; set; } = new DateTime();
        public bool Status { get; set; }
    }
}
