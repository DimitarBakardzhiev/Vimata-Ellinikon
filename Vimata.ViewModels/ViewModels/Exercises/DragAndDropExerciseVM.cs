namespace Vimata.ViewModels.ViewModels.Exercises
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class DragAndDropExerciseVM
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public IEnumerable<string> Options { get; set; }
        public bool IsGreekContent { get; set; }
        public bool AreOptionsInGreek { get; set; }
        public bool IsHearingExercise { get; set; }
    }
}
