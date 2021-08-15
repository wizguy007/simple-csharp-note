using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using simple_note.Guards;
using simple_note.User;


namespace simple_note.Auth
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            return Created("success", _authService.Register(dto));
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            try
            {

                return Ok(_authService.Login(dto));

            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

        [HttpGet("user")]
        [AuthGuard()]
        public IActionResult GetUser()
        {
            return Ok((UserEntity) Request.HttpContext.Items["User"]);
        }
    }
}
