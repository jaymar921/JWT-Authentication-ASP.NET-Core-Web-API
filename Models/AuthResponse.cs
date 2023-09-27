using JWT_Authentication_ASP.NET_Core_Web_API.Entity;

namespace JWT_Authentication_ASP.NET_Core_Web_API.Models
{
    public class AuthResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }

        public AuthResponse(User user,  string token)
        {
            Id = user.Id;
            Name = user.Name;
            Token = token;
        }
    }
}
