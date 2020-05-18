namespace Vimata.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ExercisesSession
    {
        public Guid Id { get; set; }
        public IList<Exercise> Exercises { get; set; }
    }
}
