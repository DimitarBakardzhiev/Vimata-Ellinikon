namespace Vimata.ViewModels.ViewModels.Exercises
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class LessonExercisesVM
    {
        public IEnumerable<ClosedExerciseVM> ClosedExercises { get; set; }
        public IEnumerable<OpenExerciseVM> OpenExercises { get; set; }
        public IEnumerable<DragAndDropExerciseVM> DragAndDropExercises { get; set; }
        public IEnumerable<SpeakingExerciseVM> SpeakingExercises { get; set; }
    }
}
