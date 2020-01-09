namespace Vimata.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Vimata.Data.Models;
    using Vimata.Services.Contracts;
    using Vimata.ViewModels.ViewModels.Topics;

    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class TopicsController : ControllerBase
    {
        private readonly IWebHostEnvironment env;
        private readonly ITopicService topicService;

        public TopicsController(IWebHostEnvironment env, ITopicService topicService)
        {
            this.env = env;
            this.topicService = topicService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateTopicVM topic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await this.topicService.CreateTopic(topic, env.ContentRootPath);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return Ok(await topicService.GetAll());
        }
    }
}