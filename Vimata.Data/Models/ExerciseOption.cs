namespace Vimata.Data.Models
{
    public class ExerciseOption : BaseEntity
    {
        public string Content { get; set; }

        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
    }
}
