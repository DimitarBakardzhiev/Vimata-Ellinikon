namespace Vimata.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Vimata.Data.Models;
    using Vimata.ViewModels.ViewModels;
    using Vimata.ViewModels.ViewModels.Exercises;

    public interface IExerciseService
    {
        Task CreateExercise(Exercise exercise);
        
        Task<CheckAnswerVM> CheckExercise(CheckExerciseAnswerVM exerciseAnswer);

        Task<IEnumerable<Exercise>> GetExercisesByLesson(string lesson);

        Task<Exercise> GetById(int id);

        Task EditExercise(int id, Exercise exercise);

        Task DeleteExercise(int id);

        Task<IEnumerable<Exercise>> SearchBy(ExerciseSearchCriteria criteria);
    }
}
