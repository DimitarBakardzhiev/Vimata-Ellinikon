namespace Vimata.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Exercise : BaseEntity
    {
        public string Description { get; set; }
        public string Content { get; set; }
        public string CorrectAnswer { get; set; }
        public bool TextToSpeechContent { get; set; }
        public bool TextToSpeechOptions { get; set; }
        public bool IsHearingExercise { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public ExerciseType Type { get; set; }
        public IList<ExerciseOption> Options { get; set; }
        public IList<AlternativeAnswer> AlternativeAnswers { get; set; }
    }
}
