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
    using Vimata.Services.Contracts;
    using Vimata.ViewModels.ViewModels.Lessons;

    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonService lessonService;
        private readonly IMapper mapper;

        public LessonsController(ILessonService lessonService, IMapper mapper)
        {
            this.lessonService = lessonService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetLessons()
        {
            var lessons = await this.lessonService.GetLessons();
            return Ok(this.mapper.Map<IEnumerable<LessonVM>>(lessons).OrderBy(l => l.Title));
        }

        [HttpGet]
        public async Task<IActionResult> GetMedals()
        {
            int userId = int.Parse(User.Identity.Name);
            return Ok(await this.lessonService.GetMedalsByUser(userId));
        }
    }
}