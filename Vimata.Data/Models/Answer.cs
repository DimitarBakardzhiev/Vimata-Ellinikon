namespace Vimata.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class Answer : BaseEntity
    {
        [Required]
        public string Text { get; set; }

        public int ExerciseId { get; set; }

        public Exercise Exercise { get; set; }
    }
}
