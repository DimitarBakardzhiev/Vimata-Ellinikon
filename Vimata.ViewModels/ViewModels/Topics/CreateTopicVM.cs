namespace Vimata.ViewModels.ViewModels.Topics
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class CreateTopicVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}
