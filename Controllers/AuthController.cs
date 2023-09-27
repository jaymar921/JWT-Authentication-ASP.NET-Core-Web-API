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
            var response = authenticationService.Authenticate(model);

            if(response == null)
            {
                return BadRequest(new { Message = "Username or Password is incorrect" });
            }

            return Ok(response);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = authenticationService.GetAllUsers();
            return Ok(users);
        }
    }
}
