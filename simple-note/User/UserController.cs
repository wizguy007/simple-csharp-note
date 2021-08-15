using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace simple_note.User
{
    [Route("")]
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
