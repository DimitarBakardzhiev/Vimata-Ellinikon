namespace Vimata.ViewModels.ViewModels.Exercises
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class CreateDragAndDropExerciseVM
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

        [MinLength(2)]
        public IEnumerable<string> Options { get; set; }

        [Required]
        public int LessonId { get; set; }
        public bool TextToSpeechContent { get; set; }
        public bool TextToSpeechOptions { get; set; }
        public bool IsHearingExercise { get; set; }
    }
}
