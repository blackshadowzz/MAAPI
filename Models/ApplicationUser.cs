using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MAAPI.Models
{
    public class ApplicationUser
    {
        [Key]
        public int UserId { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string UserName { get; set; } = string.Empty;
        [Column(TypeName = "varchar(100)")]
        public string? UserPassword { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? FullName { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string? UserType { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? UserEmail { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? UserImage { get; set; }
    }
    public class UserChangePassword
    {
        public int UserId { get; set; }
        public string? UserPassword { get; set; }
    }
}
