namespace Vimata.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ClosedExerciseOption : BaseEntity
    {
        public string Content { get; set; }

        public int ExerciseId { get; set; }
        public ClosedExercise Exercise { get; set; }
    }
}
