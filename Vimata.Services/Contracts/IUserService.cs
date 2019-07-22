namespace Vimata.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Vimata.Data.Models;
    using Vimata.ViewModels.Users;

    public interface IUserService
    {
        Task<User> AuthenticateAsync(string username, string password);

        Task<User> SignupUser(SignupVM newUser);

        Task<bool> IsUsedEmail(string email);

        Task<bool> ExistsUser(string email);
    }
}
