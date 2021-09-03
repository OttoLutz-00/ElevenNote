using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    //model for the user enter into when creating a note, we can't and won't need the user to provide everything, we just want the title and content, not the id, datecreated, or the owner id
    public class NoteCreate
    {
        [Required]
        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(100, ErrorMessage = "There are too many characters in this field.")]
        public string Title { get; set; }
        [MaxLength(8000)]
        public string Content { get; set; }

    }
}
