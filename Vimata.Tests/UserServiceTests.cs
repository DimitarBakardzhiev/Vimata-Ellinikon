namespace Vimata.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Vimata.Common;
    using Vimata.Data.Models;
    using Vimata.Data.Repositories;
    using Vimata.Services.Implementations;
    using Vimata.ViewModels.Users;
    using Xunit;

    public class UserServiceTests : BaseTests
    {
        public UserServiceTests() : base()
        {
        }

        [Theory]
        [InlineData("adsfarege", false)]
        [InlineData("", false)]
        [InlineData("dsa", true)]
        [InlineData("asd", true)]
        public async Task ExistsUserIsTrueWhenExistingAndFalseWhenNotExisting(string email, bool exists)
        {
            // Arrange
            var users = new List<User>();
            users.Add(new User { Email = "asd" });
            users.Add(new User { Email = "dsa" });

            var db = GetInMemoryDb();
            db.Users.AddRange(users);
            db.SaveChanges();

            var repo = new Repository<User>(db);

            var userServices = new UserService(appSettings, repo, null);

            // Act
            bool result = await userServices.ExistsUser(email);

            // Assert
            Assert.Equal(exists, result);
        }

        [Theory]
        [InlineData("", "", false)]
        [InlineData("test@abv.bg", "gG1@3561fa", true)]
        [InlineData("faef", "gG1@3561fa", false)]
        [InlineData("test@abv.bg", "1234", false)]
        public async Task AuthenticateExistingUserWithCorrectEmailAndPasswordAndRejectWrongInput(string email, string password, bool shouldAuthenticate)
        {
            // Arrange
            var db = GetInMemoryDb();
            var usersRepo = new Repository<User>(db);
            var userService = new UserService(appSettings, usersRepo, null);
            var user = new User
            {
                Email = "test@abv.bg",
                Username = "test@abv.bg",
                Password = Hasher.GetHashString("gG1@3561fa"),
                Role = Roles.User
            };

            db.Users.Add(user);
            db.SaveChanges();

            // Act
            var authUser = await userService.AuthenticateAsync(email, password);

            // Assert
            Assert.Equal(shouldAuthenticate, authUser != null);

            if (authUser != null)
            {
                Assert.NotNull(authUser.Token);
            }
        }

        [Fact]
        public async Task SignUpUserShouldCreateUserEntity()
        {
            // Arrange
            var db = GetInMemoryDb();
            var usersRepo = new Repository<User>(db);
            var userService = new UserService(appSettings, usersRepo, null);

            string email = "pesho@abv.bg";
            string password = "asdfASF@!1";

            // Act
            await userService.SignupUser(new SignupVM { Email = email, Password = password, ConfirmPassword = password });

            // Assert
            Assert.Equal(1, await db.Users.CountAsync());
            Assert.NotNull(await db.Users.FirstOrDefaultAsync(u => u.Email == email));
        }

        [Fact]
        public async Task GenerateNewPasswordChangesUserPassword()
        {
            // Arrange
            var db = GetInMemoryDb();
            var repo = new Repository<User>(db);
            var user = new User() { Id = 1, Email = "asd@asd.com", Password = "234567oihgfd" };
            var userService = new UserService(appSettings, repo, null);
            await repo.AddAsync(user);
            string oldPasswordHash = user.Password;

            // Act
            await userService.GenerateNewPassword(user.Email);

            // Assert
            Assert.True(oldPasswordHash != (await repo.FirstOrDefaultAsync(u => u.Id == user.Id)).Password);
        }

        [Theory]
        [InlineData("234567oihgfd", "234567oihgfd")]
        [InlineData("asdf", "234567oihgfd")]
        public async Task IsPasswordValidWorks(string existingPassword, string inputPassword)
        {
            // Arrange
            var db = GetInMemoryDb();
            var repo = new Repository<User>(db);
            var user = new User() { Id = 1, Email = "asd@asd.com", Password = Hasher.GetHashString(existingPassword) };
            var userService = new UserService(appSettings, repo, null);
            await repo.AddAsync(user);

            // Act
            var result = await userService.IsPasswordValid(user.Id.ToString(), inputPassword);

            // Assert
            Assert.Equal(existingPassword == inputPassword, result);
        }

        [Fact]
        public async Task ChangePasswordWorks()
        {
            // Arrange
            var db = GetInMemoryDb();
            var repo = new Repository<User>(db);
            var user = new User() { Id = 1, Email = "asd@asd.com", Password = "234567oihgfd" };
            var userService = new UserService(appSettings, repo, null);
            await repo.AddAsync(user);

            // Act
            string newPassword = "12341234";
            string newPasswordHash = Hasher.GetHashString(newPassword);
            await userService.ChangePassword(user.Id.ToString(), newPassword);

            // Assert
            Assert.Equal(newPasswordHash, (await repo.GetByIdAsync(user.Id)).Password);
        }
    }
}
