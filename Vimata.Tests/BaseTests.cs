namespace Vimata.Tests
{
    using System;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Moq;
    using Vimata.Common;
    using Vimata.Data;

    public abstract class BaseTests
    {
        protected IOptions<AppSettings> appSettings;

        protected BaseTests()
        {
            var appSettingsMock = new Mock<IOptions<AppSettings>>();
            appSettingsMock.SetupGet(s => s.Value).Returns(new AppSettings { Secret = "asdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdf" });
            this.appSettings = appSettingsMock.Object;
        }

        protected VimataDbContext GetInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<VimataDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new VimataDbContext(options);
        }
    }
}
