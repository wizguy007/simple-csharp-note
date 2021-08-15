using System;
using System.Collections.Generic;
using System.Linq;

namespace simple_note.Modules.Note
{
    public class NoteRepository : INoteRepository
    {
        private readonly SimpleTodoEntities _entities;

        public NoteRepository(SimpleTodoEntities entities)
        {
            _entities = entities;
        }

        public NoteEntity Create(NoteEntity note)
        {
            _entities.Notes.Add(note);

            note.Id = _entities.SaveChanges();

            return note;
        }

        public NoteEntity Update(NoteEntity note)
        {
            _entities.Notes.Update(note);

            _entities.SaveChanges();

            return note;
        }

        public NoteEntity FindById(int id)
        {
            return _entities.Notes.Where(u => u.Id == id).FirstOrDefault();
        }

        public IEnumerable<NoteEntity> FindByUserId(int id)
        {
            return _entities.Notes.Where(u => u.UserId == id).AsEnumerable();
        }

        public void Delete(NoteEntity note)
        {
            _entities.Notes.Remove(note);

            _entities.SaveChanges();
        }

    }

    public interface INoteRepository
    {
        public NoteEntity Create(NoteEntity note);
        public NoteEntity Update(NoteEntity note);
        public IEnumerable<NoteEntity> FindByUserId(int id);
        public NoteEntity FindById(int id);
        public void Delete(NoteEntity note);
    }
}
