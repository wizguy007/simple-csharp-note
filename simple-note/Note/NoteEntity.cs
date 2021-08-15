using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using simple_note.User;

namespace simple_note.Note
{
    [Table("notes")]
    public class NoteEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [JsonIgnore]
        public UserEntity User { get; set; }

        [Required]
        public string Title { get; set; }

        public string Content { get; set; }
    }
}
