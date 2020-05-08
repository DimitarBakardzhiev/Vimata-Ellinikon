namespace Vimata.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Vimata.Services.Contracts;
    using Vimata.ViewModels.ViewModels.Exercises;

    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly IExerciseService exerciseService;

        public ExercisesController(IExerciseService exerciseService)
        {
            this.exerciseService = exerciseService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClosedExercise(CreateClosedExerciseVM exercise)
        {
            await this.exerciseService.CreateClosedExercise(exercise);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOpenExercise(CreateOpenExerciseVM exercise)
        {
            await this.exerciseService.CreateOpenExercise(exercise);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDragAndDropExercise(CreateDragAndDropExerciseVM exercise)
        {
            await this.exerciseService.CreateDragAndDropExercise(exercise);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpeakingExercise(CreateSpeakingExerciseVM exercise)
        {
            await this.exerciseService.CreateSpeakingExercise(exercise);
            return Ok();
        }

        [HttpGet("{lesson}")]
        public async Task<IActionResult> GetClosedExercises(string lesson)
        {
            var exercises = await this.exerciseService.GetClosedExercises(lesson);

            return Ok(exercises.Select(e => new ClosedExerciseVM()
            {
                Id = e.Id,
                TextToSpeechOptions = e.TextToSpeechOptions,
                Content = e.Content,
                Description = e.Description,
                TextToSpeechContent = e.TextToSpeechContent,
                IsHearingExercise = e.IsHearingExercise,
                Options = e.Options.Select(o => o.Content).ToArray()
            }));
        }

        [HttpGet("{lesson}")]
        public async Task<IActionResult> GetOpenExercises(string lesson)
        {
            var exercises = await this.exerciseService.GetOpenExercises(lesson);

            return Ok(exercises.Select(e => new OpenExerciseVM()
            {
                Id = e.Id,
                Content = e.Content,
                Description = e.Description,
                TextToSpeechContent = e.TextToSpeechContent,
                IsHearingExercise = e.IsHearingExercise
            }));
        }

        [HttpGet("{lesson}")]
        public async Task<IActionResult> GetDragAndDropExercises(string lesson)
        {
            var exercises = await this.exerciseService.GetDragAndDropExercises(lesson);

            return Ok(exercises.Select(e => new DragAndDropExerciseVM()
            {
                Id = e.Id,
                TextToSpeechOptions = e.TextToSpeechOptions,
                Content = e.Content,
                Description = e.Description,
                TextToSpeechContent = e.TextToSpeechContent,
                IsHearingExercise = e.IsHearingExercise,
                Options = e.Options.Select(o => o.Content)
            }));
        }

        [HttpGet("{lesson}")]
        public async Task<IActionResult> GetSpeakingExercises(string lesson)
        {
            var exercises = await this.exerciseService.GetSpeakingExercises(lesson);

            return Ok(exercises.Select(e => new SpeakingExerciseVM()
            {
                Id = e.Id,
                Content = e.Content,
                Description = e.Description,
                IsHearingExercise = e.IsHearingExercise
            }));
        }

        [HttpPost]
        public async Task<IActionResult> CheckClosedExercise(CheckExerciseAnswerVM exerciseAnswer)
        {
            return Ok(await this.exerciseService.CheckClosedExercise(exerciseAnswer));
        }

        [HttpPost]
        public async Task<IActionResult> CheckOpenExercise(CheckExerciseAnswerVM exerciseAnswer)
        {
            return Ok(await this.exerciseService.CheckOpenExercise(exerciseAnswer));
        }

        [HttpPost]
        public async Task<IActionResult> CheckDragAndDropExercise(CheckExerciseAnswerVM exerciseAnswer)
        {
            return Ok(await this.exerciseService.CheckDragAndDropExercise(exerciseAnswer));
        }

        [HttpPost]
        public async Task<IActionResult> CheckSpeakingExercise(CheckExerciseAnswerVM exerciseAnswer)
        {
            return Ok(await this.exerciseService.CheckSpeakingExercise(exerciseAnswer));
        }

        [HttpGet("{exerciseId}")]
        public async Task<IActionResult> EditClosed(int exerciseId)
        {
            var exercise = await this.exerciseService.GetClosedExerciseForEdit(exerciseId);

            return Ok(exercise);
        }

        [HttpPut("{exerciseId}")]
        public async Task<IActionResult> EditClosedExercise(int exerciseId, CreateClosedExerciseVM exercise)
        {
            await this.exerciseService.EditClosedExercise(exerciseId, exercise);
            return Ok();
        }

        [HttpGet("{exerciseId}")]
        public async Task<IActionResult> EditOpen(int exerciseId)
        {
            var exercise = await this.exerciseService.GetOpenExerciseForEdit(exerciseId);

            return Ok(exercise);
        }

        [HttpPut("{exerciseId}")]
        public async Task<IActionResult> EditOpenExercise(int exerciseId, CreateOpenExerciseVM exercise)
        {
            await this.exerciseService.EditOpenExercise(exerciseId, exercise);

            return Ok();
        }

        [HttpGet("{exerciseId}")]
        public async Task<IActionResult> EditDragAndDrop(int exerciseId)
        {
            var exercise = await this.exerciseService.GetDragAndDropExerciseForEdit(exerciseId);

            return Ok(exercise);
        }

        [HttpPut("{exerciseId}")]
        public async Task<IActionResult> EditDragAndDropExercise(int exerciseId, CreateDragAndDropExerciseVM exercise)
        {
            await this.exerciseService.EditDragAndDropExercise(exerciseId, exercise);

            return Ok();
        }

        [HttpGet("{exerciseId}")]
        public async Task<IActionResult> EditSpeaking(int exerciseId)
        {
            var exercise = await this.exerciseService.GetSpeakingExerciseForEdit(exerciseId);

            return Ok(exercise);
        }

        [HttpPut("{exerciseId}")]
        public async Task<IActionResult> EditSpeakingExercise(int exerciseId, CreateSpeakingExerciseVM exercise)
        {
            await this.exerciseService.EditSpeakingExercise(exerciseId, exercise);

            return Ok();
        }

        [HttpDelete("{exerciseId}")]
        public async Task<IActionResult> RemoveClosed(int exerciseId)
        {
            await this.exerciseService.DeleteClosedExercise(exerciseId);
            return Ok();
        }

        [HttpDelete("{exerciseId}")]
        public async Task<IActionResult> RemoveOpen(int exerciseId)
        {
            await this.exerciseService.DeleteOpenExercise(exerciseId);
            return Ok();
        }

        [HttpDelete("{exerciseId}")]
        public async Task<IActionResult> RemoveDragAndDrop(int exerciseId)
        {
            await this.exerciseService.DeleteDragAndDropExercise(exerciseId);
            return Ok();
        }

        [HttpDelete("{exerciseId}")]
        public async Task<IActionResult> RemoveSpeaking(int exerciseId)
        {
            await this.exerciseService.DeleteSpeakingExercise(exerciseId);
            return Ok();
        }

        [HttpGet("{lesson}")]
        public async Task<IActionResult> ExercisesForLesson(string lesson)
        {
            var exercises = await this.exerciseService.GetExercisesByLesson(lesson);

            return Ok(exercises);
        }

        [HttpPost]
        public async Task<IActionResult> Search(ExerciseSearchCriteria criteria)
        {
            var exercises = await this.exerciseService.SearchBy(criteria);

            return Ok(exercises);
        }
    }
}