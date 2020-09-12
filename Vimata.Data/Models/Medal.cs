namespace Vimata.Data.Models
{
    public class Medal : BaseEntity
    {
        public MedalType Type { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
