using System;
using System.Web;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using simple_note.Modules.User;

namespace simple_note.Modules.Note
{
    public class NoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly UserService _userService;

        public NoteService(INoteRepository noteRepository, UserService userService)
        {
            _noteRepository = noteRepository;
            _userService = userService;
        }

        public IEnumerable<NoteEntity> GetNotes(UserEntity user)
        {
            return _noteRepository.FindByUserId(user.Id);
        }

        public NoteEntity CreateNote(UserEntity user, NoteDto dto)
        {
            var note = new NoteEntity
            {
                UserId = user.Id,
                Title = dto.Title,
                Content = dto.Content
            };

            return _noteRepository.Create(note);
        }

        public NoteEntity GetNote(UserEntity user, int id)
        {
            var note = _noteRepository.FindById(id);

            if (note == null || note?.UserId != user.Id)
            {
                throw new HttpRequestException("Resource not found", null, HttpStatusCode.NotFound);
            }
            
            return note;
        }

        public NoteEntity UpdateNote(UserEntity user, NoteDto dto, int id)
        {
            var note = GetNote(user, id);

            note.Title = dto.Title;
            note.Content = dto.Content;

            return _noteRepository.Update(note);
        }

        public void DeleteNote(UserEntity user, int id)
        {
            var note = GetNote(user, id);

            _noteRepository.Delete(note);
        }
    }
}
