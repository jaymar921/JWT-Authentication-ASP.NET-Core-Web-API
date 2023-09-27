using JWT_Authentication_ASP.NET_Core_Web_API.Entity;
using JWT_Authentication_ASP.NET_Core_Web_API.Models;

namespace JWT_Authentication_ASP.NET_Core_Web_API.Services
{
    public interface IAuthenticationService
    {
        AuthResponse? Authenticate(AuthRequest model);
        IEnumerable<User> GetAllUsers();
        User? GetById(int id);
    }
}
