namespace Vimata.Data.Models
{
    using System.Collections.Generic;

    public class DragAndDropExercise : BaseEntity
    {
        public string Description { get; set; }
        public string Content { get; set; }

        public List<DragAndDropOption> Options { get; set; }

        public string CorrectAnswer { get; set; }
        public bool TextToSpeechContent { get; set; }
        public bool TextToSpeechOptions { get; set; }
        public bool IsHearingExercise { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
