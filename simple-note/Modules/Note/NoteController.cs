using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using simple_note.Guards;
using simple_note.Modules.User;

namespace simple_note.Modules.Note
{
    [Route("/api/notes")]
    [ApiController]
    [AuthGuard()]
    public class NoteController : Controller
    {
        private readonly NoteService _noteService;

        public NoteController(NoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet()]
        public IActionResult Index()
        {
            UserEntity user = (UserEntity)Request.HttpContext.Items["User"];

            var response = _noteService.GetNotes(user);

            return Ok(response);
        }

        [HttpPost()]
        public IActionResult Store([FromBody]NoteDto dto)
        {
            UserEntity user = (UserEntity)Request.HttpContext.Items["User"];

            var response = _noteService.CreateNote(user, dto);

            return Created("success", response);
        }

        [HttpGet("{id}")]
        public IActionResult Show(int id)
        {
            UserEntity user = (UserEntity)Request.HttpContext.Items["User"];

            var response = _noteService.GetNote(user, id);

            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody]NoteDto dto, int id)
        {
            UserEntity user = (UserEntity)Request.HttpContext.Items["User"];

            var response = _noteService.UpdateNote(user, dto, id);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Destroy(int id)
        {
            UserEntity user = (UserEntity)Request.HttpContext.Items["User"];

            _noteService.DeleteNote(user, id);

            return Ok(new
            {
                status = true,
                message = "success",
            });
        }
    }
}
