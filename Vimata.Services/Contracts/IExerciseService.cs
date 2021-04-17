namespace Vimata.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Vimata.Data.Models;
    using Vimata.ViewModels.ViewModels;
    using Vimata.ViewModels.ViewModels.Exercises;
    using Vimata.ViewModels.ViewModels.Lessons;

    public interface IExerciseService
    {
        Task CreateExercise(Exercise exercise);
        
        Task<CheckAnswerVM> CheckExercise(int exerciseId, string answer);

        Task<IEnumerable<Exercise>> GetExercisesByLesson(string lesson);

        Task<Exercise> GetById(int id);

        Task EditExercise(int id, Exercise exercise);

        Task DeleteExercise(int id);

        Task<IEnumerable<Exercise>> SearchBy(ExerciseSearchCriteria criteria);

        Task<MedalType> ProcessResult(ExercisesSession session, int userId);
    }
}
