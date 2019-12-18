namespace Vimata.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Vimata.Data.Models;
    using Vimata.Services.Contracts;
    using Vimata.ViewModels.Users;
    using Vimata.ViewModels.ViewModels.Users;

    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return Ok("works");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody]SigninVM userParam)
        {
            if (await userService.ExistsUser(userParam.Email) == false)
            {
                return NotFound();
            }

            var user = await userService.AuthenticateAsync(userParam.Email, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        public IActionResult TestAuth()
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Signup([FromBody]SignupVM newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(newUser);
            }

            if (await userService.IsUsedEmail(newUser.Email))
            {
                return Conflict();
            }

            var user = await userService.SignupUser(newUser);
            user = await userService.AuthenticateAsync(newUser.Email, newUser.Password);

            return Ok(user);
        }
    }
}