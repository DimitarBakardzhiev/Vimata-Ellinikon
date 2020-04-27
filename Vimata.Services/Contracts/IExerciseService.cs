namespace Vimata.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Vimata.Data.Models;
    using Vimata.ViewModels.ViewModels.Exercises;

    public interface IExerciseService
    {
        Task CreateClosedExercise(CreateClosedExerciseVM exercise);
        Task CreateOpenExercise(CreateOpenExerciseVM exercise);
        Task CreateDragAndDropExercise(CreateDragAndDropExerciseVM exercise);
        Task CreateSpeakingExercise(CreateSpeakingExerciseVM exercise);

        Task<IEnumerable<ClosedExercise>> GetClosedExercises(string lesson);
        Task<IEnumerable<OpenExercise>> GetOpenExercises(string lesson);
        Task<IEnumerable<DragAndDropExercise>> GetDragAndDropExercises(string lesson);
        Task<IEnumerable<SpeakingExercise>> GetSpeakingExercises(string lesson);

        Task<CheckAnswerVM> CheckClosedExercise(CheckExerciseAnswerVM exerciseAnswer);
        Task<CheckAnswerVM> CheckOpenExercise(CheckExerciseAnswerVM exerciseAnswer);
        Task<CheckAnswerVM> CheckDragAndDropExercise(CheckExerciseAnswerVM exerciseAnswer);
        Task<CheckAnswerVM> CheckSpeakingExercise(CheckExerciseAnswerVM exerciseAnswer);
    }
}
