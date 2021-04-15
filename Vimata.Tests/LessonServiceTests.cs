namespace Vimata.Tests
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Vimata.Data;
    using Vimata.Data.Models;
    using Vimata.Data.Repositories;
    using Vimata.Services.Implementations;
    using Xunit;

    public class LessonServiceTests : BaseTests
    {
        public LessonServiceTests() : base()
        {
        }

        [Theory]
        [InlineData("lesson 1")]
        [InlineData("Lesson 1")]
        [InlineData("LESSON 1")]
        public async Task GetLessonByNameIsCaseInsensitive(string lessonTitle)
        {
            // Arrange
            var db = SetupLessonsDatabase(1);
            var repo = new Repository<Lesson>(db);
            var lesson = await repo.GetByIdAsync(1);
            var service = new LessonService(repo, null);

            // Act
            var lessonFromDb = await service.GetLessonByName(lessonTitle);

            // Assert
            Assert.Equal(lesson.Title, lessonFromDb.Title);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public async Task GetLessonsReturnsAllEntities(int count)
        {
            // Arrange
            var db = SetupLessonsDatabase(count);
            var repo = new Repository<Lesson>(db);
            var service = new LessonService(repo, null);

            // Act
            var lessons = await service.GetLessons();

            // Assert
            Assert.Equal(lessons.Count(), count);
        }

        [Fact]
        public async Task GetMedalsByUserWorks()
        {
            // Arrange
            var db = SetupLessonsUserMedalsDatabase();
            var repo = new Repository<Lesson>(db);
            var service = new LessonService(repo, new Repository<Medal>(db));
            var user = await db.Users.FindAsync(1);

        }

        private VimataDbContext SetupLessonsDatabase(int count)
        {
            var db = GetInMemoryDb();

            for (int i = 0; i < count; i++)
            {
                db.Add(new Lesson() { Id = i + 1, Title = $"Lesson {i + 1}" });
            }

            db.SaveChanges();
            return db;
        }

        private VimataDbContext SetupLessonsUserMedalsDatabase()
        {
            var db = SetupLessonsDatabase(4);
            var user = new User() { Id = 1, Email = "asd@asd.com", Password = "12341234" };
            db.Add(user);

            db.Add(new Medal() { Id = 1, LessonId = 1, UserId = user.Id, Type = MedalType.Gold });
            db.Add(new Medal() { Id = 2, LessonId = 2, UserId = user.Id, Type = MedalType.Silver });
            db.Add(new Medal() { Id = 3, LessonId = 3, UserId = user.Id, Type = MedalType.Bronze });
            db.SaveChanges();

            return db;
        }
    }
}
