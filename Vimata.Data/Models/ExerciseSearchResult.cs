namespace Vimata.Data.Models
{
    using System.Collections.Generic;

    public class ExerciseSearchResult
    {
        public IEnumerable<ClosedExercise> ClosedExercises { get; set; }
        public IEnumerable<OpenExercise> OpenExercises { get; set; }
        public IEnumerable<DragAndDropExercise> DragAndDropExercises { get; set; }
        public IEnumerable<SpeakingExercise> SpeakingExercises { get; set; }
    }
}
