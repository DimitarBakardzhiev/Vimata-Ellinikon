namespace Vimata.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using Vimata.Data.Models;
    using Vimata.Services.Contracts;
    using Vimata.ViewModels.ViewModels;
    using Vimata.ViewModels.ViewModels.Exercises;

    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly IExerciseService exerciseService;
        private readonly IMapper mapper;
        private readonly IMemoryCache cache;

        public ExercisesController(IExerciseService exerciseService,
            IMapper mapper,
            IMemoryCache memoryCache)
        {
            this.exerciseService = exerciseService;
            this.mapper = mapper;
            this.cache = memoryCache;
        }

        #region create
        [HttpPost]
        public async Task<IActionResult> CreateClosedExercise(CreateClosedExerciseVM exercise)
        {
            await this.exerciseService.CreateExercise(this.mapper.Map<Exercise>(exercise));
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOpenExercise(CreateOpenExerciseVM exercise)
        {
            await this.exerciseService.CreateExercise(this.mapper.Map<Exercise>(exercise));
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDragAndDropExercise(CreateDragAndDropExerciseVM exercise)
        {
            await this.exerciseService.CreateExercise(this.mapper.Map<Exercise>(exercise));
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpeakingExercise(CreateSpeakingExerciseVM exercise)
        {
            await this.exerciseService.CreateExercise(this.mapper.Map<Exercise>(exercise));
            return Ok();
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> CheckExercise(CheckExerciseAnswerVM exerciseAnswer)
        {
            var session = this.cache.Get<ExercisesSession>(exerciseAnswer.SessionId);
            if (session == null)
            {
                return BadRequest("Session not found!");
            }

            var exercise = session.Exercises.FirstOrDefault(e => e.Id == exerciseAnswer.ExerciseId);
            if (exercise == null)
            {
                return BadRequest("This exercise is not from this session!");
            }

            session.Exercises.Remove(exercise);
            this.cache.Set(exerciseAnswer.SessionId, session, new MemoryCacheEntryOptions() { SlidingExpiration = TimeSpan.FromMinutes(15) });

            return Ok(await this.exerciseService.CheckExercise(exerciseAnswer));
        }

        #region edit
        [HttpGet("{exerciseId}")]
        public async Task<IActionResult> Edit(int exerciseId)
        {
            var exercise = await this.exerciseService.GetById(exerciseId);

            switch (exercise.Type)
            {
                case ExerciseType.Closed:
                    return Ok(this.mapper.Map<CreateClosedExerciseVM>(exercise));
                case ExerciseType.Open:
                    return Ok(this.mapper.Map<CreateOpenExerciseVM>(exercise));
                case ExerciseType.DragAndDrop:
                    return Ok(this.mapper.Map<CreateDragAndDropExerciseVM>(exercise));
                default:
                    return Ok(this.mapper.Map<CreateSpeakingExerciseVM>(exercise));
            }
        }

        [HttpPut("{exerciseId}")]
        public async Task<IActionResult> EditClosedExercise(int exerciseId, CreateClosedExerciseVM exercise)
        {
            await this.exerciseService.EditExercise(exerciseId, this.mapper.Map<Exercise>(exercise));
            return Ok();
        }

        [HttpPut("{exerciseId}")]
        public async Task<IActionResult> EditOpenExercise(int exerciseId, CreateOpenExerciseVM exercise)
        {
            await this.exerciseService.EditExercise(exerciseId, this.mapper.Map<Exercise>(exercise));

            return Ok();
        }

        [HttpPut("{exerciseId}")]
        public async Task<IActionResult> EditDragAndDropExercise(int exerciseId, CreateDragAndDropExerciseVM exercise)
        {
            await this.exerciseService.EditExercise(exerciseId, this.mapper.Map<Exercise>(exercise));

            return Ok();
        }

        [HttpPut("{exerciseId}")]
        public async Task<IActionResult> EditSpeakingExercise(int exerciseId, CreateSpeakingExerciseVM exercise)
        {
            await this.exerciseService.EditExercise(exerciseId, this.mapper.Map<Exercise>(exercise));

            return Ok();
        }
        #endregion

        [HttpDelete("{exerciseId}")]
        public async Task<IActionResult> Remove(int exerciseId)
        {
            await this.exerciseService.DeleteExercise(exerciseId);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Search(ExerciseSearchCriteria criteria)
        {
            var exercises = await this.exerciseService.SearchBy(criteria);

            var result = new List<ExerciseSearchResultVM>();
            result.AddRange(this.mapper.Map<IEnumerable<Exercise>, IEnumerable<ExerciseSearchResultVM>>(exercises));

            return Ok(result);
        }

        [HttpGet("{lesson}")]
        [AllowAnonymous]
        public async Task<IActionResult> SetSession(string lesson)
        {
            var exercises = await this.exerciseService.GetExercisesByLesson(lesson);

            var closedExercises = exercises.Where(e => e.Type == ExerciseType.Closed);
            var openExercises = exercises.Where(e => e.Type == ExerciseType.Open);
            var dragAndDropExercises = exercises.Where(e => e.Type == ExerciseType.DragAndDrop);
            var speakingExercises = exercises.Where(e => e.Type == ExerciseType.Speaking);

            var session = new ExercisesSession()
            {
                Id = Guid.NewGuid(),
                Exercises = new List<Exercise>(exercises)
            };

            //HttpContext.Session.SetObject(session.Id.ToString(), session);
            this.cache.Set(session.Id.ToString(), session, new MemoryCacheEntryOptions() { SlidingExpiration = TimeSpan.FromMinutes(15) });
            return Ok(new ExercisesSessionVM()
            {
                Id = session.Id,
                ClosedExercises = this.mapper.Map<IEnumerable<ClosedExerciseVM>>(closedExercises),
                OpenExercises = this.mapper.Map<IEnumerable<OpenExerciseVM>>(openExercises),
                DragAndDropExercises = this.mapper.Map<IEnumerable<DragAndDropExerciseVM>>(dragAndDropExercises),
                SpeakingExercises = this.mapper.Map<IEnumerable<SpeakingExerciseVM>>(speakingExercises) 
            });
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetSession(string id)
        {
            //var session = HttpContext.Session.GetObject<ExercisesSession>(id);
            var session = this.cache.Get<ExercisesSession>(id);

            var closedExercises = session.Exercises.Where(e => e.Type == ExerciseType.Closed);
            var openExercises = session.Exercises.Where(e => e.Type == ExerciseType.Open);
            var dragAndDropExercises = session.Exercises.Where(e => e.Type == ExerciseType.DragAndDrop);
            var speakingExercises = session.Exercises.Where(e => e.Type == ExerciseType.Speaking);

            return Ok(new ExercisesSessionVM()
            {
                Id = session.Id,
                ClosedExercises = this.mapper.Map<IEnumerable<ClosedExerciseVM>>(closedExercises),
                OpenExercises = this.mapper.Map<IEnumerable<OpenExerciseVM>>(openExercises),
                DragAndDropExercises = this.mapper.Map<IEnumerable<DragAndDropExerciseVM>>(dragAndDropExercises),
                SpeakingExercises = this.mapper.Map<IEnumerable<SpeakingExerciseVM>>(speakingExercises)
            });
        }
    }
}