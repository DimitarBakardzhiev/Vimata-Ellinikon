namespace Vimata.ViewModels.ViewModels.Exercises
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class CreateOpenExerciseVM
    {
        [Required]
        [MinLength(4)]
        public string Description { get; set; }

        [Required]
        [MinLength(4)]
        public string Content { get; set; }

        [Required]
        [MinLength(1)]
        public string CorrectAnswer { get; set; }

        [Required]
        [MinLength(4)]
        public string Lesson { get; set; }
        public bool TextToSpeechContent { get; set; }
        public bool IsHearingExercise { get; set; }

        public IEnumerable<string> AlternativeAnswers { get; set; }
    }
}
