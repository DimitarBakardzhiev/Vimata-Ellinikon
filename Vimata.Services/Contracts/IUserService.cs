﻿namespace Vimata.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Vimata.Data.Models;
    using Vimata.ViewModels.Users;
    using Vimata.ViewModels.ViewModels.Users;

    public interface IUserService
    {
        Task<AuthenticationVM> AuthenticateAsync(string username, string password);

        Task<User> SignupUser(SignupVM newUser);

        Task<bool> ExistsUser(string email);

        Task SendResetPasswordConfirmationEmail(string email, string resetUrl);

        Task SendNewPasswordEmail(string email, string newPassword);

        Task ChangePassword(string userId, string newPassword);

        Task<string> GenerateNewPassword(string email);

        Task<bool> IsPasswordValid(string userId, string password);

        Task UpdateUserData(int userId, UpdateUserVM user);

        Task<UpdateUserVM> GetUserData(int userId);
    }
}
