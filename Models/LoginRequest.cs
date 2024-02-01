using System.ComponentModel.DataAnnotations.Schema;

namespace MAAPI.Models
{
    public class LoginRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string? UserPassword { get; set; }
    }
}
