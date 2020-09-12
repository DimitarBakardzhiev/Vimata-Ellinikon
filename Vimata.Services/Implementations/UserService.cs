namespace Vimata.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using Vimata.Data.Models;
    using Vimata.Services.Contracts;
    using System.Linq;
    using Vimata.Common;
    using Microsoft.Extensions.Options;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;
    using System.Security.Claims;
    using Vimata.ViewModels.Users;
    using Vimata.Data.Repositories;
    using System.Threading.Tasks;
    using Vimata.ViewModels.ViewModels.Users;

    public class UserService : IUserService
    {
        private readonly AppSettings appSettings;
        private readonly IRepository<User> usersRepository;

        public UserService(IOptions<AppSettings> appSettings, IRepository<User> usersRepository)
        {
            this.appSettings = appSettings.Value;
            this.usersRepository = usersRepository;
        }

        public async Task<AuthenticationVM> AuthenticateAsync(string email, string password)
        {
            var user = await usersRepository.FirstOrDefaultAsync(u => u.Email == email && u.Password == Hasher.GetHashString(password));

            if (user == null)
            {
                return null;
            }

            var token = GetJwtToken(user);

            return new AuthenticationVM
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Token = token
            };
        }

        private string GetJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> ExistsUser(string email)
        {
            return await usersRepository.FirstOrDefaultAsync(u => u.Email == email) != null;
        }

        public async Task<User> SignupUser(SignupVM newUser)
        {
            var user = new User
            {
                Email = newUser.Email,
                Username = newUser.Email,
                Password = Hasher.GetHashString(newUser.Password),
                Role = Roles.User
            };

            await usersRepository.AddAsync(user);
            return user;
        }
    }
}
