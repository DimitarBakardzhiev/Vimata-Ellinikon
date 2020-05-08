namespace Vimata.ViewModels.ViewModels.Exercises
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ClosedExerciseVM
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public bool TextToSpeechContent { get; set; }
        public bool TextToSpeechOptions { get; set; }
        public bool IsHearingExercise { get; set; }
        public string[] Options { get; set; }
    }
}
