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

        public LessonType LessonTitle { get; set; }
    }
}
