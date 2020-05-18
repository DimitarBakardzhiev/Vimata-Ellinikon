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
    using Vimata.ViewModels.ViewModels.Exercises;
    using Microsoft.EntityFrameworkCore;
    using Vimata.ViewModels.ViewModels;

    public class ExerciseService : IExerciseService
    {
        private readonly IRepository<Exercise> exercisesReporitory;
        private readonly IRepository<Lesson> lessonRepository;

        public ExerciseService(IRepository<Exercise> exercisesReporitory,
            IRepository<Lesson> lessonRepository)
        {
            this.exercisesReporitory = exercisesReporitory;
            this.lessonRepository = lessonRepository;
        }

        #region CreateExercises
        public async Task CreateExercise(Exercise exercise)
        {
            await this.exercisesReporitory.AddAsync(exercise);
        }
        #endregion

        #region CheckAnswer
        public async Task<CheckAnswerVM> CheckExercise(CheckExerciseAnswerVM exerciseAnswer)
        {
            var exercise = await this.exercisesReporitory
                .GetWhere(e => e.Id == exerciseAnswer.ExerciseId)
                .Include(e => e.Options)
                .Include(e => e.AlternativeAnswers)
                .FirstOrDefaultAsync();

            if (exercise.CorrectAnswer.ToLowerInvariant() == exerciseAnswer.Answer.ToLower())
            {
                return new CheckAnswerVM() { IsCorrect = true, CorrectAnswer = exercise.CorrectAnswer };
            }

            if (exercise.AlternativeAnswers.Count > 0)
            {
                foreach (var alternative in exercise.AlternativeAnswers)
                {
                    if (exerciseAnswer.Answer.ToLower() == alternative.Content.ToLower())
                    {
                        return new CheckAnswerVM() { IsCorrect = true, CorrectAnswer = exercise.CorrectAnswer };
                    }
                }
            }

            return new CheckAnswerVM() { IsCorrect = false, CorrectAnswer = exercise.CorrectAnswer };
        }
        #endregion

        #region GetById
        public async Task<Exercise> GetById(int id)
        {
            return await this.exercisesReporitory.GetWhere(e => e.Id == id)
                .Include(e => e.Lesson)
                .Include(e => e.AlternativeAnswers)
                .Include(e => e.Options)
                .FirstOrDefaultAsync();
        }
        #endregion

        #region edit
        public async Task EditExercise(int id, Exercise editedExercise)
        {
            var exercise = await this.GetById(id);

            exercise.IsHearingExercise = editedExercise.IsHearingExercise;
            exercise.TextToSpeechContent = editedExercise.TextToSpeechContent;
            exercise.TextToSpeechOptions = editedExercise.TextToSpeechOptions;
            exercise.Content = editedExercise.Content;
            exercise.CorrectAnswer = editedExercise.CorrectAnswer;
            exercise.Description = editedExercise.Description;
            exercise.LessonId = editedExercise.LessonId;
            exercise.Options = editedExercise.Options;
            exercise.AlternativeAnswers = editedExercise.AlternativeAnswers;

            await this.exercisesReporitory.UpdateAsync(exercise);
        }
        #endregion

        #region delete
        public async Task DeleteExercise(int id)
        {
            var exercise = await this.exercisesReporitory.GetByIdAsync(id);

            await this.exercisesReporitory.RemoveAsync(exercise);
        }
        #endregion

        public async Task<IEnumerable<Exercise>> SearchBy(ExerciseSearchCriteria criteria)
        {
            var exercises = await this.exercisesReporitory
                .GetWhere(e => string.IsNullOrEmpty(criteria.Lesson) || e.Lesson.Title.ToLower() == criteria.Lesson.ToLower())
                .Where(e => string.IsNullOrEmpty(criteria.Description) || e.Description.ToLower().Contains(criteria.Description.ToLower()))
                .Where(e => string.IsNullOrEmpty(criteria.Content) || e.Content.ToLower().Contains(criteria.Content.ToLower()))
                .Include(e => e.Lesson)
                .Include(e => e.Options)
                .Include(e => e.AlternativeAnswers)
                .ToArrayAsync();

            return exercises;
        }

        public async Task<IEnumerable<Exercise>> GetExercisesByLesson(string lesson)
        {
            var exercises = await this.exercisesReporitory.GetWhere(e => e.Lesson.Title.ToLower() == lesson.ToLower())
                .Include(e => e.Lesson)
                .Include(e => e.AlternativeAnswers)
                .Include(e => e.Options)
                .ToArrayAsync();

            return exercises;
        }
    }
}
