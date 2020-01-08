namespace Vimata.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Exercise : BaseEntity
    {
        public ExerciseType Type { get; set; }

        public string Description { get; set; }

        public ICollection<Answer> Answers { get; set; }

        public string CorrectAnswer { get; set; }

        public int TopicId { get; set; }

        public Topic Topic { get; set; }
    }
}
