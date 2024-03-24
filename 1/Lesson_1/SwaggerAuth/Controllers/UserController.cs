using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SwaggerAuth.DataStore.Entity;
using SwaggerAuth.Services.Abstractions;

namespace SwaggerAuth.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route(template:"Login")]
        public IActionResult Login([FromBody] string login, string password)
        {
            var token = _userService.UserCheck(login, password);

            if (!token.IsNullOrEmpty())
            {                
                return Ok(token);
            }
            return NotFound("User not found");
        }

        
    }
}
