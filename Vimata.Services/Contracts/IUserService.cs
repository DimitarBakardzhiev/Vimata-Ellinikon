namespace Vimata.Services.Contracts
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
    }
}
