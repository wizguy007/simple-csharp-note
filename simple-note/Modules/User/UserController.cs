using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using simple_note.Guards;

namespace simple_note.Modules.User
{
    [Route("/api/user")]
    [AuthGuard()]
    [ApiController]
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Success");
        }
    }
}
