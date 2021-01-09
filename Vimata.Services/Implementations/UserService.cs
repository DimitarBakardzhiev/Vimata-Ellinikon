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
    using MailKit.Net.Smtp;
    using System.Net.Mail;
    using MimeKit;
    using MimeKit.Text;

    public class UserService : IUserService
    {
        private readonly AppSettings appSettings;
        private readonly IRepository<User> usersRepository;
        private readonly IEmailService emailService;

        public UserService(IOptions<AppSettings> appSettings, IRepository<User> usersRepository, IEmailService emailService)
        {
            this.appSettings = appSettings.Value;
            this.usersRepository = usersRepository;
            this.emailService = emailService;
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

        public async Task SendResetPasswordConfirmationEmail(string email, string resetUrl)
        {
            await this.emailService.SendEmail(
                email,
                "Забравена парола",
                $"Беше изпратена заявка за забравена парола. Ако не сте направили тази заявка игнорирайте това съобщение. За получаване на нова парола натиснете линка {resetUrl}");
        }
        public async Task SendNewPasswordEmail(string email, string newPassword)
        {
            await this.emailService.SendEmail(email, "Забравена парола", $"Вашата нова парола е: {newPassword}");
        }

        public async Task ChangePassword(string userId, string newPassword)
        {
            var user = await this.usersRepository.GetByIdAsync(int.Parse(userId));
            user.Password = Hasher.GetHashString(newPassword);
            await usersRepository.UpdateAsync(user);
        }
        public async Task<bool> IsPasswordValid(string userId, string password)
        {
            var user = await this.usersRepository.GetByIdAsync(int.Parse(userId));
            return user.Password == Hasher.GetHashString(password);
        }

        public async Task<string> GenerateNewPassword(string email)
        {
            var user = this.usersRepository.GetWhere(u => u.Email.ToLower() == email.ToLower()).FirstOrDefault();
            string newPassword = RandomPassword(7);
            user.Password = Hasher.GetHashString(newPassword);
            await usersRepository.UpdateAsync(user);

            return newPassword;
        }

        private string RandomPassword(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var password = new StringBuilder();
            var random = new Random();

            for (int i = 0; i < length; i++)
            {
                password.Append(chars[random.Next(chars.Length)]);
            }

            return password.ToString();
        }
    }
}
