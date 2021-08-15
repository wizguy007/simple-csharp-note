using System;
using Microsoft.EntityFrameworkCore;
using simple_note.Modules.Note;
using simple_note.Modules.User;

namespace simple_note
{
    public class SimpleTodoEntities : DbContext
    {
        public SimpleTodoEntities(DbContextOptions<SimpleTodoEntities> options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { set; get; }

        public DbSet<NoteEntity> Notes { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(entity => {
                entity.HasIndex(e => e.Email).IsUnique();
            });     
        }
    }
}
