namespace Vimata.Data.Models
{
    public class SpeakingExercise : BaseEntity
    {
        public string Description { get; set; }
        public string Content { get; set; }
        public string CorrectAnswer { get; set; }
        public bool IsHearingExercise { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
