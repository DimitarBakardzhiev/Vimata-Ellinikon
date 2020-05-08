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
                IsHearingExercise = exercise.IsHearingExercise,
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

        #region GetById
        public async Task<CreateClosedExerciseVM> GetClosedExerciseForEdit(int id)
        {
            var exercise = await this.closedExercisesReporitory.GetWhere(e => e.Id == id).Include(e => e.Options).Include(e => e.Lesson).FirstOrDefaultAsync();

            return new CreateClosedExerciseVM()
            {
                Content = exercise.Content,
                CorrectAnswer = exercise.CorrectAnswer,
                Description = exercise.Description,
                IsHearingExercise = exercise.IsHearingExercise,
                Lesson = exercise.Lesson.Title,
                TextToSpeechContent = exercise.TextToSpeechContent,
                TextToSpeechOptions = exercise.TextToSpeechOptions,
                Options = exercise.Options.Select(o => o.Content)
            };
        }

        public async Task<CreateOpenExerciseVM> GetOpenExerciseForEdit(int id)
        {
            var exercise = await this.openExercisesReporitory.GetWhere(e => e.Id == id).Include(e => e.AlternativeAnswers).Include(e => e.Lesson).FirstOrDefaultAsync();

            return new CreateOpenExerciseVM()
            {
                Description = exercise.Description,
                Content = exercise.Content,
                CorrectAnswer = exercise.CorrectAnswer,
                IsHearingExercise = exercise.IsHearingExercise,
                TextToSpeechContent = exercise.TextToSpeechContent,
                Lesson = exercise.Lesson.Title,
                AlternativeAnswers = exercise.AlternativeAnswers.Select(a => a.Content).ToArray()
            };
        }

        public async Task<CreateDragAndDropExerciseVM> GetDragAndDropExerciseForEdit(int id)
        {
            var exercise = await this.dragAndDropExercisesReporitory.GetWhere(e => e.Id == id).Include(e => e.Lesson).Include(e => e.Options).FirstOrDefaultAsync();

            return new CreateDragAndDropExerciseVM()
            {
                Description = exercise.Description,
                Content = exercise.Content,
                CorrectAnswer = exercise.CorrectAnswer,
                TextToSpeechContent = exercise.TextToSpeechContent,
                TextToSpeechOptions = exercise.TextToSpeechOptions,
                IsHearingExercise = exercise.IsHearingExercise,
                Lesson = exercise.Lesson.Title,
                Options = exercise.Options.Select(o => o.Content).ToArray()
            };
        }

        public async Task<CreateSpeakingExerciseVM> GetSpeakingExerciseForEdit(int id)
        {
            var exercise = await this.speakingExercisesReporitory.GetWhere(e => e.Id == id).Include(e => e.Lesson).FirstOrDefaultAsync();

            return new CreateSpeakingExerciseVM()
            {
                Description = exercise.Description,
                Content = exercise.Content,
                CorrectAnswer = exercise.CorrectAnswer,
                IsHearingExercise = exercise.IsHearingExercise,
                Lesson = exercise.Lesson.Title
            };
        }
        #endregion

        #region edit
        public async Task EditClosedExercise(int id, CreateClosedExerciseVM editedExercise)
        {
            var exercise = await this.closedExercisesReporitory.GetWhere(e => e.Id == id).Include(e => e.Lesson).Include(e => e.Options).FirstOrDefaultAsync();

            exercise.IsHearingExercise = editedExercise.IsHearingExercise;
            exercise.TextToSpeechContent = editedExercise.TextToSpeechContent;
            exercise.TextToSpeechOptions = editedExercise.TextToSpeechOptions;
            exercise.Content = editedExercise.Content;
            exercise.CorrectAnswer = editedExercise.CorrectAnswer;
            exercise.Description = editedExercise.Description;
            exercise.Lesson = this.lessonRepository.GetWhere(l => l.Title == editedExercise.Lesson).FirstOrDefault();
            exercise.Options = editedExercise.Options.Select(o => new ClosedExerciseOption() { Content = o }).ToList();

            await this.closedExercisesReporitory.UpdateAsync(exercise);
        }

        public async Task EditOpenExercise(int id, CreateOpenExerciseVM editedExercise)
        {
            var exercise = await this.openExercisesReporitory.GetWhere(e => e.Id == id).Include(e => e.Lesson).Include(e => e.AlternativeAnswers).FirstOrDefaultAsync();

            exercise.Description = editedExercise.Description;
            exercise.Content = editedExercise.Content;
            exercise.CorrectAnswer = editedExercise.CorrectAnswer;
            exercise.IsHearingExercise = editedExercise.IsHearingExercise;
            exercise.TextToSpeechContent = editedExercise.TextToSpeechContent;
            exercise.Lesson = this.lessonRepository.GetWhere(l => l.Title == editedExercise.Lesson).FirstOrDefault();
            exercise.AlternativeAnswers = editedExercise.AlternativeAnswers.Select(a => new OpenExerciseAlternativeAnswer() { Content = a }).ToList();

            await this.openExercisesReporitory.UpdateAsync(exercise);
        }

        public async Task EditDragAndDropExercise(int id, CreateDragAndDropExerciseVM editedExercise)
        {
            var exercise = await this.dragAndDropExercisesReporitory.GetWhere(e => e.Id == id).Include(e => e.Lesson).Include(e => e.Options).FirstOrDefaultAsync();

            exercise.Description = editedExercise.Description;
            exercise.Content = editedExercise.Content;
            exercise.CorrectAnswer = editedExercise.CorrectAnswer;
            exercise.IsHearingExercise = editedExercise.IsHearingExercise;
            exercise.TextToSpeechContent = editedExercise.TextToSpeechContent;
            exercise.TextToSpeechOptions = editedExercise.TextToSpeechOptions;
            exercise.Lesson = this.lessonRepository.GetWhere(l => l.Title == editedExercise.Lesson).FirstOrDefault();
            exercise.Options = editedExercise.Options.Select(o => new DragAndDropOption() { Content = o }).ToList();

            await this.dragAndDropExercisesReporitory.UpdateAsync(exercise);
        }

        public async Task EditSpeakingExercise(int id, CreateSpeakingExerciseVM editedExercise)
        {
            var exercise = await this.speakingExercisesReporitory.GetWhere(e => e.Id == id).Include(e => e.Lesson).FirstOrDefaultAsync();

            exercise.Description = editedExercise.Description;
            exercise.Content = editedExercise.Content;
            exercise.CorrectAnswer = editedExercise.CorrectAnswer;
            exercise.IsHearingExercise = editedExercise.IsHearingExercise;
            exercise.Lesson = this.lessonRepository.GetWhere(l => l.Title == editedExercise.Lesson).FirstOrDefault();

            await this.speakingExercisesReporitory.UpdateAsync(exercise);
        }
        #endregion

        #region delete
        public async Task DeleteClosedExercise(int id)
        {
            var exercise = await this.closedExercisesReporitory.GetByIdAsync(id);

            await this.closedExercisesReporitory.RemoveAsync(exercise);
        }

        public async Task DeleteOpenExercise(int id)
        {
            var exercise = await this.openExercisesReporitory.GetByIdAsync(id);

            await this.openExercisesReporitory.RemoveAsync(exercise);
        }

        public async Task DeleteDragAndDropExercise(int id)
        {
            var exercise = await this.dragAndDropExercisesReporitory.GetByIdAsync(id);

            await this.dragAndDropExercisesReporitory.RemoveAsync(exercise);
        }

        public async Task DeleteSpeakingExercise(int id)
        {
            var exercise = await this.speakingExercisesReporitory.GetByIdAsync(id);

            await this.speakingExercisesReporitory.RemoveAsync(exercise);
        }
        #endregion

        public async Task<IEnumerable<ExerciseSearchResultVM>> GetExercisesByLesson(string lesson)
        {
            var exercises = new List<ExerciseSearchResultVM>();
            exercises.AddRange(await this.closedExercisesReporitory
                .GetWhere(e => e.Lesson.Title.ToLower() == lesson.ToLower())
                .Select(e => new ExerciseSearchResultVM { Content = e.Content, Description = e.Description, ExerciseId = e.Id, Type = ExerciseSearchResultVM.ExerciseType.Closed })
                .ToArrayAsync());

            exercises.AddRange(await this.openExercisesReporitory
                .GetWhere(e => e.Lesson.Title.ToLower() == lesson.ToLower())
                .Select(e => new ExerciseSearchResultVM { Content = e.Content, Description = e.Description, ExerciseId = e.Id, Type = ExerciseSearchResultVM.ExerciseType.Open })
                .ToArrayAsync());

            exercises.AddRange(await this.dragAndDropExercisesReporitory
                .GetWhere(e => e.Lesson.Title.ToLower() == lesson.ToLower())
                .Select(e => new ExerciseSearchResultVM { Content = e.Content, Description = e.Description, ExerciseId = e.Id, Type = ExerciseSearchResultVM.ExerciseType.DragAndDrop })
                .ToArrayAsync());

            exercises.AddRange(await this.speakingExercisesReporitory
                .GetWhere(e => e.Lesson.Title.ToLower() == lesson.ToLower())
                .Select(e => new ExerciseSearchResultVM { Content = e.Content, Description = e.Description, ExerciseId = e.Id, Type = ExerciseSearchResultVM.ExerciseType.Speaking })
                .ToArrayAsync());

            return exercises;
        }

        public async Task<IEnumerable<ExerciseSearchResultVM>> SearchBy(ExerciseSearchCriteria criteria)
        {
            var exercises = new List<ExerciseSearchResultVM>();

            exercises.AddRange(await this.closedExercisesReporitory
                .GetWhere(e => string.IsNullOrEmpty(criteria.Lesson) || e.Lesson.Title.ToLower() == criteria.Lesson.ToLower())
                .Where(e => string.IsNullOrEmpty(criteria.Description) || e.Description.ToLower().Contains(criteria.Description.ToLower()))
                .Where(e => string.IsNullOrEmpty(criteria.Content) || e.Content.ToLower().Contains(criteria.Content.ToLower()))
                .Select(e => new ExerciseSearchResultVM
                { 
                    ExerciseId = e.Id,
                    Description = e.Description,
                    Content = e.Content,
                    Type = ExerciseSearchResultVM.ExerciseType.Closed,
                    Lesson = e.Lesson.Title })
                .ToArrayAsync());

            exercises.AddRange(await this.openExercisesReporitory
                .GetWhere(e => string.IsNullOrEmpty(criteria.Lesson) || e.Lesson.Title.ToLower() == criteria.Lesson.ToLower())
                .Where(e => string.IsNullOrEmpty(criteria.Description) || e.Description.ToLower().Contains(criteria.Description.ToLower()))
                .Where(e => string.IsNullOrEmpty(criteria.Content) || e.Content.ToLower().Contains(criteria.Content.ToLower()))
                .Select(e => new ExerciseSearchResultVM
                {
                    ExerciseId = e.Id,
                    Description = e.Description,
                    Content = e.Content,
                    Type = ExerciseSearchResultVM.ExerciseType.Open,
                    Lesson = e.Lesson.Title
                })
                .ToArrayAsync());

            exercises.AddRange(await this.dragAndDropExercisesReporitory
                .GetWhere(e => string.IsNullOrEmpty(criteria.Lesson) || e.Lesson.Title.ToLower() == criteria.Lesson.ToLower())
                .Where(e => string.IsNullOrEmpty(criteria.Description) || e.Description.ToLower().Contains(criteria.Description.ToLower()))
                .Where(e => string.IsNullOrEmpty(criteria.Content) || e.Content.ToLower().Contains(criteria.Content.ToLower()))
                .Select(e => new ExerciseSearchResultVM
                {
                    ExerciseId = e.Id,
                    Description = e.Description,
                    Content = e.Content,
                    Type = ExerciseSearchResultVM.ExerciseType.DragAndDrop,
                    Lesson = e.Lesson.Title
                })
                .ToArrayAsync());

            exercises.AddRange(await this.speakingExercisesReporitory
                .GetWhere(e => string.IsNullOrEmpty(criteria.Lesson) || e.Lesson.Title.ToLower() == criteria.Lesson.ToLower())
                .Where(e => string.IsNullOrEmpty(criteria.Description) || e.Description.ToLower().Contains(criteria.Description.ToLower()))
                .Where(e => string.IsNullOrEmpty(criteria.Content) || e.Content.ToLower().Contains(criteria.Content.ToLower()))
                .Select(e => new ExerciseSearchResultVM
                {
                    ExerciseId = e.Id,
                    Description = e.Description,
                    Content = e.Content,
                    Type = ExerciseSearchResultVM.ExerciseType.Speaking,
                    Lesson = e.Lesson.Title
                })
                .ToArrayAsync());

            return exercises;
        }
    }
}
