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

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody]SigninVM userParam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid user data");
            }

            if (await userService.ExistsUser(userParam.Email) == false)
            {
                return NotFound();
            }

            var user = await userService.AuthenticateAsync(userParam.Email, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Signup([FromBody]SignupVM newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(newUser);
            }

            if (await userService.ExistsUser(newUser.Email))
            {
                return Conflict();
            }

            await userService.SignupUser(newUser);
            var user = await userService.AuthenticateAsync(newUser.Email, newUser.Password);

            return Ok(user);
        }
    }
}