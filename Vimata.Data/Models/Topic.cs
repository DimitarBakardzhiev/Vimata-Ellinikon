namespace Vimata.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class Topic : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        public string Image { get; set; }

        public ICollection<Exercise> Exercises { get; set; }
    }
}
