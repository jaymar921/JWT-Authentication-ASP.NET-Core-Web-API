using JWT_Authentication_ASP.NET_Core_Web_API.Models;
using JWT_Authentication_ASP.NET_Core_Web_API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWT_Authentication_ASP.NET_Core_Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private IAuthenticationService authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        // Login
        [HttpPost("login")]
        public IActionResult Login(AuthRequest model)
        {
            // Authenticate the user and get the response
            var response = authenticationService.Authenticate(model);

            if(response == null)
            {
                // if user is not authenticated, return unauthorized
                return Unauthorized(new { Message = "Username or Password is incorrect" });
            }

            // success
            return Ok(response);
        }

        // Retrieve All users
        // But let's make sure that the user accessing this resource is authenticated
        // so we have to place [Authorize] tag in the route to ensure that the user is authenticated
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = authenticationService.GetAllUsers();
            return Ok(users);
        }
    }
}
