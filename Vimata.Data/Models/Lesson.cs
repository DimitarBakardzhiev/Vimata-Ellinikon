namespace Vimata.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Lesson : BaseEntity
    {
        public enum LessonType
        {
            Alphabet
        }

        public string Title { get; set; }

        public List<ClosedExercise> ClosedExercises { get; set; }
        public List<DragAndDropExercise> DragAndDropExercises { get; set; }
        public List<OpenExercise> OpenExercises { get; set; }
        public List<SpeakingExercise> SpeakingExercises { get; set; }
    }
}
