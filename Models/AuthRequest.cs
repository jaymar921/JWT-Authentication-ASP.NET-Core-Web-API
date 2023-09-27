using System.ComponentModel.DataAnnotations;

namespace JWT_Authentication_ASP.NET_Core_Web_API.Models
{
    public class AuthRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
