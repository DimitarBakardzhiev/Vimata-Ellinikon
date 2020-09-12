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
    using Microsoft.EntityFrameworkCore;

    public class LessonService : ILessonService
    {
        private readonly IRepository<Lesson> lessonRepository;
        private readonly IRepository<Medal> medalReporsitory;

        public LessonService(IRepository<Lesson> lessonRepository, IRepository<Medal> medalRepository)
        {
            this.lessonRepository = lessonRepository;
            this.medalReporsitory = medalRepository;
        }

        public async Task<Lesson> GetLessonByName(string lesson)
        {
            return await this.lessonRepository.FirstOrDefaultAsync(l => l.Title.ToLower() == lesson.ToLower());
        }

        public async Task<IEnumerable<Lesson>> GetLessons()
        {
            var lessons = await this.lessonRepository.GetAllAsync();

            return lessons;
        }

        public async Task<IList<LessonMedalVM>> GetMedalsByUser(int userId)
        {
            var medals = await this.medalReporsitory.GetWhere(m => m.UserId == userId).Select(m => new LessonMedalVM { Lesson = m.Lesson.Title, Medal = m.Type }).ToListAsync();

            return medals;
        }
    }
}
