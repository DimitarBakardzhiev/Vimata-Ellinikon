namespace Vimata.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Moq;
    using Vimata.Common;
    using Vimata.Data;
    using Vimata.Data.Models;
    using Vimata.Data.Repositories;
    using Vimata.Services.Implementations;
    using Vimata.ViewModels.Users;
    using Xunit;

    public class UserServiceTests
    {
        private IOptions<AppSettings> appSettings;

        public UserServiceTests()
        {
            var appSettingsMock = new Mock<IOptions<AppSettings>>();
            appSettingsMock.SetupGet(s => s.Value).Returns(new AppSettings { Secret = "asdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdf" });
            this.appSettings = appSettingsMock.Object;
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

        private VimataDbContext GetInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<VimataDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new VimataDbContext(options);
        }
    }
}
