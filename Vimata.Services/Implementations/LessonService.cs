namespace Vimata.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading.Tasks;
    using Vimata.Data.Models;
    using Vimata.Data.Repositories;
    using Vimata.Services.Contracts;
    using Vimata.ViewModels.ViewModels.Lessons;

    public class LessonService : ILessonService
    {
        private readonly IRepository<Lesson> lessonRepository;

        public LessonService(IRepository<Lesson> lessonRepository)
        {
            this.lessonRepository = lessonRepository;
        }

        public async Task<IEnumerable<string>> GetLessons()
        {
            var lessons = await this.lessonRepository.GetAllAsync();

            return lessons.Select(l => l.Title.ToString()).OrderBy(l => l).ToArray();
        }
    }
}
