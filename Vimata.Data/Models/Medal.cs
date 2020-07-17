namespace Vimata.Data.Models
{
    using System.Collections.Generic;

    public class Medal : BaseEntity
    {
        public MedalType Type { get; set; }

        public List<MedalUserLesson> UserLesson { get; set; }
    }
}
