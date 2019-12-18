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

    public class UserService : IUserService
    {
        private readonly AppSettings appSettings;
        private readonly IRepository<User> usersRepository;

        public UserService(IOptions<AppSettings> appSettings, IRepository<User> usersRepository)
        {
            this.appSettings = appSettings.Value;
            this.usersRepository = usersRepository;
        }

        public async Task<User> AuthenticateAsync(string email, string password)
        {
            if (email == null || password == null)
            {
                return null;
            }

            var user = await usersRepository.FirstOrDefaultAsync(u => u.Email == email && u.Password == Hasher.GetHashString(password)); //_users.SingleOrDefault(x => x.Username == username && x.Password == password);

            if (user == null)
            {
                return null;
            }

            // authentication successful so generate jwt token
            user.Token = GetJwtToken(user);

            // remove password before returning
            user.Password = null;

            return user;
        }

        private string GetJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
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
                Password = Hasher.GetHashString(newUser.Password)
            };

            await usersRepository.AddAsync(user);
            return user;
        }

        public async Task<bool> IsUsedEmail(string email)
        {
            return await usersRepository.CountWhereAsync(u => u.Email == email) > 0;
        }
    }
}
