namespace Vimata.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Medal : BaseEntity
    {
        public enum MedalType
        {
            Gold,
            Silver,
            Bronze
        }

        public MedalType Type { get; set; }

        public List<MedalUserLesson> UserLesson { get; set; }
    }
}
