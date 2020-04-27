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
                AreOptionsInGreek = e.TextToSpeechOptions,
                Content = e.Content,
                Description = e.Description,
                IsGreekContent = e.TextToSpeechContent,
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
                IsGreekContent = e.TextToSpeechContent,
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
                AreOptionsInGreek = e.TextToSpeechOptions,
                Content = e.Content,
                Description = e.Description,
                IsGreekContent = e.TextToSpeechContent,
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
    }
}