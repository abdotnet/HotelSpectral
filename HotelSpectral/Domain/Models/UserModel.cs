using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HotelSpectral.Domain.Models
{
    public class UserModel : BaseModel
    {
        public String Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public String ConfirmPassword { get; set; }

        public int UserType { get; set; }
        public DateTime LastLoginDate { get; set; } = new DateTime();
        public String PictureName { get; set; }
        public bool Status { get; set; }
        //public int RoleId { get; set; }
    }
    public class UserReqModel
    {
        [Required]
        public String FirstName { get; set; }
        [Required]
        public String LastName { get; set; }
        public String OtherNames { get; set; }
        [Required]
        public string DOB { get; set; }
        [Required]
        public int Gender { get; set; }
        public int Title { get; set; }
        [Required]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(16, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 5)]
        //[RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
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
        public String Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public String ConfirmPassword { get; set; }
        public int UserType { get; set; }
        public String PictureName { get; set; }
    }

    public class UserAdminModel : UserReqModel // UserModel
    {
        [Required]
        public int RoleId { get; set; }
    }

    public class RoleModel
    {
        public int Id { get; set; }
        [Required]
        public String RoleName { get; set; }
        public DateTime CreatedDate { get; set; } = new DateTime();
        public bool Status { get; set; }
    }

    public class UserRoleModel
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int RoleId { get; set; }
        public DateTime CreatedDate { get; set; } = new DateTime();
        public bool Status { get; set; }
    }

    public class PermissionModel
    {
        public int Id { get; set; }
        public String Action { get; set; }
        public String Descrition { get; set; }
        public DateTime CreatedDate { get; set; } = new DateTime();
        public bool Status { get; set; }
    }
    public class RolePermissionModel
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public DateTime CreatedDate { get; set; } = new DateTime();
        public bool Status { get; set; }
    }
}
