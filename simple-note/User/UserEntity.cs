using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using simple_note.Note;

namespace simple_note.User
{
    [Table("users")]
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [StringLength(200)]
        [Required]
        public string Email { get; set; }

        [Required]
        [JsonIgnore]
        public string Password { get; set; }

        public ICollection<NoteEntity> ?Notes { get; set; }

    }
}
