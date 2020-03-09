namespace Vimata.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class MedalUserLesson
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int MedalId { get; set; }
        public Medal Medal { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
