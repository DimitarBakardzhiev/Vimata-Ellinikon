namespace Vimata.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using Vimata.Data.Models;
    using Vimata.Services.Contracts;
    using Vimata.ViewModels.Users;
    using Vimata.ViewModels.ViewModels.Users;

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMemoryCache cache;

        public UsersController(IUserService userService, IMemoryCache cache)
        {
            this.userService = userService;
            this.cache = cache;
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
            var user = await userService.AuthenticateAsync(newUser.Email.ToLower(), newUser.Password);

            return Ok(user);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> ResetPasswordConfirmation(string email)
        {
            if (await userService.ExistsUser(email))
            {
                string key = Guid.NewGuid().ToString();
                string url = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                this.cache.Set(key, email, new MemoryCacheEntryOptions() { SlidingExpiration = TimeSpan.FromHours(3) });

                await userService.SendResetPasswordConfirmationEmail(email, $"{url}/api/users/resetPassword/{key}"); // url must match ResetPassword action

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> ResetPassword(string key)
        {
            string email = this.cache.Get<string>(key);

            if (email == null)
            {
                return BadRequest();
            }
            else
            {
                string newPassword = await this.userService.GenerateNewPassword(email);

                try
                {
                    await this.userService.SendNewPasswordEmail(email, newPassword);
                }
                catch (Exception)
                {
                    return Ok("Възникна проблем при изпращането на новата Ви парола! Моля, уведомете администратор!");
                }

                return Ok("Новата Ви парола е изпратена на Вашия имейл.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordVM changePassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userId = User.Identity.Name;
            if (await this.userService.IsPasswordValid(userId, changePassword.OldPassword))
            {
                await this.userService.ChangePassword(userId, changePassword.Password);
                return Ok();
            }

            return Forbid();
        }
    }
}