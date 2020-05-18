namespace Vimata.ViewModels.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Vimata.Data.Models;

    public class ExerciseSearchResultVM
    {
        public int ExerciseId { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Lesson { get; set; }
        public ExerciseType Type { get; set; }
    }
}
