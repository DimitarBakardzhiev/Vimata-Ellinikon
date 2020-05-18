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
    using Vimata.Web.Extensions;

    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        class ExercisesSession
        {
            public Guid Id { get; set; }
            public IList<ClosedExerciseVM> ClosedExercises { get; set; }
            public IList<OpenExerciseVM> OpenExercises { get; set; }
            public IList<DragAndDropExerciseVM> DragAndDropExercises { get; set; }
            public IList<SpeakingExerciseVM> SpeakingExercises { get; set; }
        }

        private readonly IExerciseService exerciseService;
        private readonly IMapper mapper;
        private List<ExercisesSession> exercisesSessions = new List<ExercisesSession>();
        private readonly IMemoryCache cache;

        public ExercisesController(IExerciseService exerciseService, IMapper mapper, IMemoryCache memoryCache)
        {
            this.exerciseService = exerciseService;
            this.mapper = mapper;
            this.cache = memoryCache;
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
            return Ok(this.mapper.Map<IEnumerable<ClosedExercise>, IEnumerable<ClosedExerciseVM>>(exercises));
        }

        [HttpGet("{lesson}")]
        public async Task<IActionResult> GetOpenExercises(string lesson)
        {
            var exercises = await this.exerciseService.GetOpenExercises(lesson);
            return Ok(this.mapper.Map<IEnumerable<OpenExercise>, IEnumerable<OpenExerciseVM>>(exercises));
        }

        [HttpGet("{lesson}")]
        public async Task<IActionResult> GetDragAndDropExercises(string lesson)
        {
            var exercises = await this.exerciseService.GetDragAndDropExercises(lesson);
            return Ok(this.mapper.Map<IEnumerable<DragAndDropExercise>, IEnumerable<DragAndDropExerciseVM>>(exercises));
        }

        [HttpGet("{lesson}")]
        public async Task<IActionResult> GetSpeakingExercises(string lesson)
        {
            var exercises = await this.exerciseService.GetSpeakingExercises(lesson);
            return Ok(this.mapper.Map<IEnumerable<SpeakingExercise>, IEnumerable<SpeakingExerciseVM>>(exercises));
        }

        [HttpPost]
        public async Task<IActionResult> CheckClosedExercise([FromBody]CheckExerciseAnswerVM exerciseAnswer)
        {
            var session = this.cache.Get<ExercisesSession>(exerciseAnswer.SessionId);
            if (session == null)
            {
                return BadRequest("Session not found!");
            }

            var exercise = session.ClosedExercises.FirstOrDefault(e => e.Id == exerciseAnswer.ExerciseId);
            if (exercise == null)
            {
                return BadRequest("This exercise is not from this session!");
            }

            session.ClosedExercises.Remove(exercise);
            this.cache.Set(exerciseAnswer.SessionId, session, new MemoryCacheEntryOptions() { SlidingExpiration = TimeSpan.FromMinutes(15) });

            return Ok(await this.exerciseService.CheckClosedExercise(exerciseAnswer));
        }

        [HttpPost]
        public async Task<IActionResult> CheckOpenExercise(CheckExerciseAnswerVM exerciseAnswer)
        {
            var session = this.cache.Get<ExercisesSession>(exerciseAnswer.SessionId);
            if (session == null)
            {
                return BadRequest("Session not found!");
            }

            var exercise = session.OpenExercises.FirstOrDefault(e => e.Id == exerciseAnswer.ExerciseId);
            if (exercise == null)
            {
                return BadRequest("This exercise is not from this session!");
            }

            session.OpenExercises.Remove(exercise);
            this.cache.Set(exerciseAnswer.SessionId, session, new MemoryCacheEntryOptions() { SlidingExpiration = TimeSpan.FromMinutes(15) });

            return Ok(await this.exerciseService.CheckOpenExercise(exerciseAnswer));
        }

        [HttpPost]
        public async Task<IActionResult> CheckDragAndDropExercise(CheckExerciseAnswerVM exerciseAnswer)
        {
            var session = this.cache.Get<ExercisesSession>(exerciseAnswer.SessionId);
            if (session == null)
            {
                return BadRequest("Session not found!");
            }

            var exercise = session.DragAndDropExercises.FirstOrDefault(e => e.Id == exerciseAnswer.ExerciseId);
            if (exercise == null)
            {
                return BadRequest("This exercise is not from this session!");
            }

            session.DragAndDropExercises.Remove(exercise);
            this.cache.Set(exerciseAnswer.SessionId, session, new MemoryCacheEntryOptions() { SlidingExpiration = TimeSpan.FromMinutes(15) });

            return Ok(await this.exerciseService.CheckDragAndDropExercise(exerciseAnswer));
        }

        [HttpPost]
        public async Task<IActionResult> CheckSpeakingExercise(CheckExerciseAnswerVM exerciseAnswer)
        {
            var session = this.cache.Get<ExercisesSession>(exerciseAnswer.SessionId);
            if (session == null)
            {
                return BadRequest("Session not found!");
            }

            var exercise = session.SpeakingExercises.FirstOrDefault(e => e.Id == exerciseAnswer.ExerciseId);
            if (exercise == null)
            {
                return BadRequest("This exercise is not from this session!");
            }

            session.SpeakingExercises.Remove(exercise);
            this.cache.Set(exerciseAnswer.SessionId, session, new MemoryCacheEntryOptions() { SlidingExpiration = TimeSpan.FromMinutes(15) });

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

        [HttpPost]
        public async Task<IActionResult> Search(ExerciseSearchCriteria criteria)
        {
            var exercises = await this.exerciseService.SearchBy(criteria);

            var result = new List<ExerciseSearchResultVM>();
            result.AddRange(this.mapper.Map<IEnumerable<ClosedExercise>, IEnumerable<ExerciseSearchResultVM>>(exercises.ClosedExercises));
            result.AddRange(this.mapper.Map<IEnumerable<OpenExercise>, IEnumerable<ExerciseSearchResultVM>>(exercises.OpenExercises));
            result.AddRange(this.mapper.Map<IEnumerable<DragAndDropExercise>, IEnumerable<ExerciseSearchResultVM>>(exercises.DragAndDropExercises));
            result.AddRange(this.mapper.Map<IEnumerable<SpeakingExercise>, IEnumerable<ExerciseSearchResultVM>>(exercises.SpeakingExercises));

            return Ok(result);
        }

        [HttpGet("{lesson}")]
        [AllowAnonymous]
        public async Task<IActionResult> SetSession(string lesson)
        {
            var closedExercises = await this.exerciseService.GetClosedExercises(lesson);
            var openExercises = await this.exerciseService.GetOpenExercises(lesson);
            var dragAndDropExercises = await this.exerciseService.GetDragAndDropExercises(lesson);
            var speakingExercises = await this.exerciseService.GetSpeakingExercises(lesson);

            var session = new ExercisesSession()
            {
                Id = Guid.NewGuid(),
                ClosedExercises = this.mapper.Map<IEnumerable<ClosedExercise>, IList<ClosedExerciseVM>>(closedExercises),
                OpenExercises = this.mapper.Map<IEnumerable<OpenExercise>, IList<OpenExerciseVM>>(openExercises),
                DragAndDropExercises = this.mapper.Map<IEnumerable<DragAndDropExercise>, IList<DragAndDropExerciseVM>>(dragAndDropExercises),
                SpeakingExercises = this.mapper.Map<IEnumerable<SpeakingExercise>, IList<SpeakingExerciseVM>>(speakingExercises),
            };

            //HttpContext.Session.SetObject(session.Id.ToString(), session);
            this.cache.Set(session.Id.ToString(), session, new MemoryCacheEntryOptions() { SlidingExpiration = TimeSpan.FromMinutes(15) });
            return Ok(session);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetSession(string id)
        {
            //var session = HttpContext.Session.GetObject<ExercisesSession>(id);
            var session = this.cache.Get<ExercisesSession>(id);
            return Ok(session);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetTest()
        {
            var exercises = await this.exerciseService.GetSpeakingExercises("азбука");
            return Ok(exercises.Select(e => new SpeakingExerciseVM()
            {
                Id = e.Id,
                Content = e.Content,
                Description = e.Description,
                IsHearingExercise = e.IsHearingExercise
            }).FirstOrDefault());
        }
    }
}