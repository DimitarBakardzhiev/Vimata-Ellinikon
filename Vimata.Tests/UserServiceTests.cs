namespace Vimata.Tests
{
    using Microsoft.Extensions.Options;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Vimata.Common;
    using Vimata.Data.Models;
    using Vimata.Data.Repositories;
    using Vimata.Services.Contracts;
    using Vimata.Services.Implementations;

    public class UserServiceTests
    {
        [Test]
        public async Task ExistsUserIsTrueWhenExisting()
        {
            var users = new List<User>();
            users.Add(new User { Email = "asd" });
            users.Add(new User { Email = "dsa" });

            var repositoryMock = new Mock<IRepository<User>>();
            string user = "asd";
            repositoryMock.Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(users.FirstOrDefault(u => u.Email == user));
            var optionsMock = new Mock<IOptions<AppSettings>>();
            optionsMock.Setup(o => o.Value).Returns(new AppSettings());
            var userServices = new UserService(optionsMock.Object, repositoryMock.Object);

            var result = await userServices.ExistsUser(user);

            Assert.AreEqual(result, true);
        }

        [Test]
        public async Task ExistsUserIsFalseWhenNotExisting()
        {
            var users = new List<User>();
            users.Add(new User { Email = "asd" });
            users.Add(new User { Email = "dsa" });

            var repositoryMock = new Mock<IRepository<User>>();
            string user = "asdfasdf";
            repositoryMock.Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(users.FirstOrDefault(u => u.Email == user));
            var optionsMock = new Mock<IOptions<AppSettings>>();
            optionsMock.Setup(o => o.Value).Returns(new AppSettings());
            var userServices = new UserService(optionsMock.Object, repositoryMock.Object);

            var result = await userServices.ExistsUser(user);

            Assert.AreNotEqual(result, true);
        }
    }
}
