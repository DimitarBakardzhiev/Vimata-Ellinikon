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
        private readonly IRepository<User> userRepository;

        public LessonService(IRepository<Lesson> lessonRepository, IRepository<User> userRepository)
        {
            this.lessonRepository = lessonRepository;
            this.userRepository = userRepository;
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
            var user = await this.userRepository.GetWhere(u => u.Id == userId)
                .Include(u => u.MedalLesson).ThenInclude(ml => ml.Lesson)
                .Include(u => u.MedalLesson).ThenInclude(ml => ml.Medal)
                .FirstOrDefaultAsync();

            var medals = new List<LessonMedalVM>();
            foreach (var item in user.MedalLesson)
            {
                medals.Add(new LessonMedalVM() { Lesson = item.Lesson.Title, Medal = item.Medal.Type });
            }

            return medals;
        }
    }
}
