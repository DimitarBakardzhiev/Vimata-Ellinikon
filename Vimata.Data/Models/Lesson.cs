namespace Vimata.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Lesson : BaseEntity
    {
        public string Title { get; set; }

        public List<Exercise> Exercises { get; set; }
    }
}
