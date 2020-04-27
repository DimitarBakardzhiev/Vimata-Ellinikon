namespace Vimata.ViewModels.ViewModels.Exercises
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class CheckExerciseAnswerVM
    {
        [Required]
        public int ExerciseId { get; set; }

        [Required]
        [MinLength(1)]
        public string Answer { get; set; }
    }
}
