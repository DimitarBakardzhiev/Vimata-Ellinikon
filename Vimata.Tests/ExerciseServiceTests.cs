namespace Vimata.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
 
    using Vimata.Data.Models;
    using Vimata.Data.Repositories;
    using Vimata.Services.Implementations;
    using Vimata.ViewModels.ViewModels.Exercises;
    
    using Xunit;

    public class ExerciseServiceTests : BaseTests
    {
        [Fact]
        public async Task CreateExerciseShouldCreateEntity()
        {
            // arrange
            var db = this.GetInMemoryDb();
            var repo = new Repository<Exercise>(db);
            var service = new ExerciseService(repo, null, null, null);

            // act
            await service.CreateExercise(new Exercise()
            {
                Content = "asd",
                CorrectAnswer = "asd",
                Description = "asd",
                Id = 1,
                IsHearingExercise = false,
                Type = ExerciseType.Closed
            });

            // assert
            var exercise = await repo.GetByIdAsync(1);
            Assert.NotNull(exercise);
            Assert.Equal("asd", exercise.Content);
        }

        [Theory]
        [InlineData("asdf", "asdf", true)]
        [InlineData("ASDF", "asdf", true)]
        [InlineData("asdf alternative", "asdf", true)]
        [InlineData("dsa", "asdf", false)]
        public async Task CheckExerciseChecksAnswerAndAlternativeAnswersCaseInsensitive(string givenAnswer, string correctAnswer, bool expectedResult)
        {
            // arrange
            var db = this.GetInMemoryDb();
            var repo = new Repository<Exercise>(db);
            var service = new ExerciseService(repo, null, null, null);
            await repo.AddAsync(new Exercise()
            {
                Content = "asd",
                CorrectAnswer = correctAnswer,
                AlternativeAnswers = new List<AlternativeAnswer>() { new AlternativeAnswer { Content = "asdf alternative" } },
                Id = 1,
                Description = "asd",
                Type = ExerciseType.Speaking
            });

            // act
            var result = await service.CheckExercise(new CheckExerciseAnswerVM { ExerciseId = 1, Answer = givenAnswer });

            // assert
            Assert.NotNull(result);
            Assert.Equal(result.IsCorrect, expectedResult);
        }

        [Fact]
        public async Task GetByIdReturnsExercise()
        {
            // arrange
            var db = this.GetInMemoryDb();
            var repo = new Repository<Exercise>(db);
            var service = new ExerciseService(repo, null, null, null);
            await repo.AddAsync(new Exercise()
            {
                Content = "asd",
                CorrectAnswer = "afsd",
                AlternativeAnswers = new List<AlternativeAnswer>() { new AlternativeAnswer { Content = "asdf alternative" } },
                Id = 1,
                Description = "asd",
                Type = ExerciseType.Speaking,
                Lesson = new Lesson() { Id = 1, Title = "asd" },
                Options = null
            });

            // act
            var exercise = await service.GetById(1);

            // assert
            Assert.NotNull(exercise);
            Assert.Equal("asd", exercise.Description);
        }

        [Fact]
        public async Task EditExerciseUpdatesEntity()
        {
            // arrange
            var db = this.GetInMemoryDb();
            var repo = new Repository<Exercise>(db);
            var service = new ExerciseService(repo, null, null, null);
            var exercise = new Exercise()
            {
                Content = "asd",
                CorrectAnswer = "afsd",
                AlternativeAnswers = new List<AlternativeAnswer>() { new AlternativeAnswer { Content = "asdf alternative" } },
                Id = 1,
                Description = "asd",
                Type = ExerciseType.Speaking,
                Lesson = new Lesson() { Id = 1, Title = "asd" },
                Options = null
            };
            await repo.AddAsync(exercise);

            // act
            string newContent = "lorem ipsun dolor";
            exercise.Content = newContent;
            await service.EditExercise(1, exercise);

            // assert
            var editedExercise = await repo.GetByIdAsync(1);
            Assert.Equal(newContent, editedExercise.Content);
        }

        [Fact]
        public async Task DeleteExerciseRemovesEntity()
        {
            // arrange
            var db = this.GetInMemoryDb();
            var repo = new Repository<Exercise>(db);
            var service = new ExerciseService(repo, null, null, null);
            await repo.AddAsync(new Exercise()
            {
                Content = "asd",
                CorrectAnswer = "afsd",
                AlternativeAnswers = new List<AlternativeAnswer>() { new AlternativeAnswer { Content = "asdf alternative" } },
                Id = 1,
                Description = "asd",
                Type = ExerciseType.Speaking,
                Lesson = new Lesson() { Id = 1, Title = "asd" },
                Options = null
            });

            // act
            await service.DeleteExercise(1);

            // arrange
            var exercise = await repo.GetByIdAsync(1);
            Assert.Null(exercise);
        }

        [Theory]
        [InlineData(1, "asd", "asd", true)]
        [InlineData(2, "asd", "asd", false)]
        [InlineData(1, "ASd", "ASD", true)]
        [InlineData(1, "asd", "asdddd", false)]
        [InlineData(0, "asd", null, true)]
        [InlineData(0, null, null, true)]
        public async Task SearchByWorksWithAllConditionsCaseInsensitive(int lessonId, string content, string description, bool shouldFindExercises)
        {
            // arrange
            var db = this.GetInMemoryDb();
            var repo = new Repository<Exercise>(db);
            var service = new ExerciseService(repo, null, null, null);
            await repo.AddAsync(new Exercise()
            {
                Content = "asd",
                CorrectAnswer = "afsd",
                AlternativeAnswers = new List<AlternativeAnswer>() { new AlternativeAnswer { Content = "asdf alternative" } },
                Id = 1,
                Description = "asd",
                Type = ExerciseType.Speaking,
                Lesson = new Lesson() { Id = 1, Title = "asd" },
                Options = null
            });

            // act
            var exercises = await service.SearchBy(new ExerciseSearchCriteria()
            {
                LessonId = lessonId,
                Content = content,
                Description = description
            });

            // assert
            Assert.Equal(shouldFindExercises, exercises.Count() > 0);
        }

        [Theory]
        [InlineData("asd", true)]
        [InlineData("asD", true)]
        [InlineData("dsa", false)]
        public async Task SearchByLessonReturnsExercisesIsCaseInsensitive(string lesson, bool shouldFindExercises)
        {
            // arrange
            var db = this.GetInMemoryDb();
            var repo = new Repository<Exercise>(db);
            var service = new ExerciseService(repo, null, null, null);
            await repo.AddAsync(new Exercise()
            {
                Content = "asd",
                CorrectAnswer = "afsd",
                AlternativeAnswers = new List<AlternativeAnswer>() { new AlternativeAnswer { Content = "asdf alternative" } },
                Id = 1,
                Description = "asd",
                Type = ExerciseType.Speaking,
                Lesson = new Lesson() { Id = 1, Title = "asd" },
                Options = null
            });

            // act
            var exercises = await service.GetExercisesByLesson(lesson);

            // assert
            Assert.Equal(shouldFindExercises, exercises.Count() > 0);
        }

        [Theory]
        [InlineData(10, true, MedalType.Gold)]
        [InlineData(9, true, MedalType.Silver)]
        [InlineData(6, true, MedalType.Bronze)]
        [InlineData(3, false, null)]
        public async Task ProcessResultAwardsCorrectMedal(int correctAnswers, bool shouldAwardMedal, MedalType expectedMedal)
        {
            // arrange
            var session = new ExercisesSession()
            {
                AnsweredCorrectly = correctAnswers,
                InitialExercisesCount = 10,
                Lesson = "asd"
            };

            var db = this.GetInMemoryDb();
            var lessonsRepo = new Repository<Lesson>(db);
            await lessonsRepo.AddAsync(new Lesson() { Id = 1, Title = "asd" });

            var usersRepo = new Repository<User>(db);
            await usersRepo.AddAsync(new User()
            {
                Id = 1,
                Email = "test@test.com",
                Password = "123441234",
                Role = "User"
            });

            var medalsRepo = new Repository<Medal>(db);
            var service = new ExerciseService(null, lessonsRepo, medalsRepo, usersRepo);

            // act
            await service.ProcessResult(session, 1);

            // assert
            var medal = await medalsRepo.FirstOrDefaultAsync(m => true);
            if (shouldAwardMedal)
            {
                Assert.NotNull(medal);
                Assert.Equal(expectedMedal, medal.Type);
            }
            else
            {
                Assert.Null(medal);
            }
        }
    }
}
