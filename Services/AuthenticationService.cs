using JWT_Authentication_ASP.NET_Core_Web_API.Entity;
using JWT_Authentication_ASP.NET_Core_Web_API.Helpers;
using JWT_Authentication_ASP.NET_Core_Web_API.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT_Authentication_ASP.NET_Core_Web_API.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppSettings _appSettings;

        public AuthenticationService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        /*
         * For simplicity, we hardcoded the user data
         * 
         * In production, user should be stored in a database and it's password should be hashed
         */
        private List<User> _users = new List<User>
        {
            new User { Id = 1, Name = "Jayharron Abejar", Username = "jay", Password = "123"}
        };
        public AuthResponse? Authenticate(AuthRequest model)
        {
            // In production, this will retrieve user in the database based on the given request model
            User? user = _users.SingleOrDefault(u => u.Username == model.UserName && u.Password == model.Password);

            // return null if user is not found
            if (user == null) return null;

            // generate a token
            var token = GenerateJwtToken(user);

            // return a token response
            return new AuthResponse(user, token);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _users;
        }

        public User? GetById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        /*
         * Helper method, for generating Json Web Token
         * 
         * install Nuget System.IdentityModel.Tokens.Jwt
         */
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // makes the Id of user to be the claim identity
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                // set the token expiry to a day
                Expires = DateTime.UtcNow.AddDays(1),
                // setting the signing credentials
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            // create the token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
