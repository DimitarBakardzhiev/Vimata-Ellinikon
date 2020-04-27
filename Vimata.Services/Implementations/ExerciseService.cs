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

    public class ExerciseService : IExerciseService
    {
        private readonly IRepository<ClosedExercise> closedExercisesReporitory;
        private readonly IRepository<Lesson> lessonRepository;
        private readonly IRepository<OpenExercise> openExercisesReporitory;
        private readonly IRepository<DragAndDropExercise> dragAndDropExercisesReporitory;
        private readonly IRepository<SpeakingExercise> speakingExercisesReporitory;

        public ExerciseService(IRepository<ClosedExercise> closedExercisesReporitory,
            IRepository<Lesson> lessonRepository,
            IRepository<OpenExercise> openExercisesReporitory,
            IRepository<DragAndDropExercise> dragAndDropExercisesReporitory,
            IRepository<SpeakingExercise> speakingExercisesReporitory)
        {
            this.closedExercisesReporitory = closedExercisesReporitory;
            this.lessonRepository = lessonRepository;
            this.openExercisesReporitory = openExercisesReporitory;
            this.dragAndDropExercisesReporitory = dragAndDropExercisesReporitory;
            this.speakingExercisesReporitory = speakingExercisesReporitory;
        }

        #region CreateExercises
        public async Task CreateClosedExercise(CreateClosedExerciseVM exercise)
        {
            await this.closedExercisesReporitory.AddAsync(new ClosedExercise()
            {
                Description = exercise.Description,
                Content = exercise.Content,
                CorrectAnswer = exercise.CorrectAnswer,
                TextToSpeechOptions = exercise.TextToSpeechOptions,
                TextToSpeechContent = exercise.TextToSpeechContent,
                IsHearingExercise = exercise.IsHearingExercise,
                Lesson = await this.lessonRepository.FirstOrDefaultAsync(l => l.Title == exercise.Lesson),
                Options = new List<ClosedExerciseOption>(exercise.Options.Select(o => new ClosedExerciseOption() { Content = o }))
            });
        }

        public async Task CreateOpenExercise(CreateOpenExerciseVM exercise)
        {
            await this.openExercisesReporitory.AddAsync(new OpenExercise
            {
                Content = exercise.Content,
                CorrectAnswer = exercise.CorrectAnswer,
                Description = exercise.Description,
                TextToSpeechContent = exercise.TextToSpeechContent,
                IsHearingExercise = exercise.isHearingExercise,
                Lesson = await this.lessonRepository.FirstOrDefaultAsync(l => l.Title == exercise.Lesson),
                AlternativeAnswers = new List<OpenExerciseAlternativeAnswer>(exercise.AlternativeAnswers.Select(a => new OpenExerciseAlternativeAnswer() { Content = a }))
            });
        }

        public async Task CreateDragAndDropExercise(CreateDragAndDropExerciseVM exercise)
        {
            await this.dragAndDropExercisesReporitory.AddAsync(new DragAndDropExercise()
            {
                TextToSpeechOptions = exercise.TextToSpeechOptions,
                Content = exercise.Content,
                CorrectAnswer = exercise.CorrectAnswer,
                Description = exercise.Description,
                TextToSpeechContent = exercise.TextToSpeechContent,
                IsHearingExercise = exercise.IsHearingExercise,
                Lesson = await this.lessonRepository.FirstOrDefaultAsync(l => l.Title == exercise.Lesson),
                Options = new List<DragAndDropOption>(exercise.Options.Select(o => new DragAndDropOption() { Content = o }))
            });
        }

        public async Task CreateSpeakingExercise(CreateSpeakingExerciseVM exercise)
        {
            await this.speakingExercisesReporitory.AddAsync(new SpeakingExercise()
            {
                Content = exercise.Content,
                CorrectAnswer = exercise.CorrectAnswer,
                Description = exercise.Description,
                IsHearingExercise = exercise.IsHearingExercise,
                Lesson = await this.lessonRepository.FirstOrDefaultAsync(l => l.Title == exercise.Lesson)
            });
        }
        #endregion

        #region GetExercisesForLesson
        public async Task<IEnumerable<ClosedExercise>> GetClosedExercises(string lesson)
        {
            return await this.closedExercisesReporitory.GetWhere(e => e.Lesson.Title.ToLower() == lesson.ToLower()).Include(e => e.Options).ToListAsync();
        }
        public async Task<IEnumerable<OpenExercise>> GetOpenExercises(string lesson)
        {
            return await this.openExercisesReporitory.GetWhere(e => e.Lesson.Title.ToLower() == lesson.ToLower()).ToListAsync();
        }

        public async Task<IEnumerable<DragAndDropExercise>> GetDragAndDropExercises(string lesson)
        {
            return await this.dragAndDropExercisesReporitory.GetWhere(e => e.Lesson.Title.ToLower() == lesson.ToLower()).Include(e => e.Options).ToListAsync();
        }

        public async Task<IEnumerable<SpeakingExercise>> GetSpeakingExercises(string lesson)
        {
            return await this.speakingExercisesReporitory.GetWhere(e => e.Lesson.Title.ToLower() == lesson.ToLower()).ToListAsync();
        }
        #endregion

        #region CheckAnswer
        public async Task<CheckAnswerVM> CheckClosedExercise(CheckExerciseAnswerVM exerciseAnswer)
        {
            var exercise = await this.closedExercisesReporitory.GetByIdAsync(exerciseAnswer.ExerciseId);

            return new CheckAnswerVM() { IsCorrect = exercise.CorrectAnswer.ToLower() == exerciseAnswer.Answer.ToLower(), CorrectAnswer = exercise.CorrectAnswer };
        }

        public async Task<CheckAnswerVM> CheckOpenExercise(CheckExerciseAnswerVM exerciseAnswer)
        {
            var exercise = await this.openExercisesReporitory.GetByIdAsync(exerciseAnswer.ExerciseId);
            bool isCorrect = exercise.CorrectAnswer.ToLower() == exerciseAnswer.Answer.ToLower();

            if (!isCorrect && exercise.AlternativeAnswers.Count > 0)
            {
                foreach (var answer in exercise.AlternativeAnswers)
                {
                    if (exerciseAnswer.Answer.ToLower() == answer.Content.ToLower())
                    {
                        isCorrect = true;
                    }
                }
            }

            return new CheckAnswerVM() { IsCorrect = isCorrect, CorrectAnswer = exercise.CorrectAnswer };
        }

        public async Task<CheckAnswerVM> CheckDragAndDropExercise(CheckExerciseAnswerVM exerciseAnswer)
        {
            var exercise = await this.dragAndDropExercisesReporitory.GetByIdAsync(exerciseAnswer.ExerciseId);

            return new CheckAnswerVM() { IsCorrect = exercise.CorrectAnswer.ToLower() == exerciseAnswer.Answer.ToLower(), CorrectAnswer = exercise.CorrectAnswer };
        }

        public async Task<CheckAnswerVM> CheckSpeakingExercise(CheckExerciseAnswerVM exerciseAnswer)
        {
            var exercise = await this.speakingExercisesReporitory.GetByIdAsync(exerciseAnswer.ExerciseId);

            return new CheckAnswerVM() { IsCorrect = exercise.CorrectAnswer.ToLower() == exerciseAnswer.Answer.ToLower(), CorrectAnswer = exercise.CorrectAnswer };
        }
        #endregion
    }
}
