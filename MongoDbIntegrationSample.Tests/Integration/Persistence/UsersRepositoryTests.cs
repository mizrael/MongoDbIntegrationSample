using System.Threading.Tasks;
using FluentAssertions;
using MongoDbIntegrationSample.Models;
using MongoDbIntegrationSample.Persistence;
using Xunit;

namespace MongoDbIntegrationSample.Tests.Integration.Persistence
{
    public class UsersRepositoryTests : IClassFixture<DbFixture>
    {
        private readonly DbFixture _fixture;

        public UsersRepositoryTests(DbFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task FindByEmail_should_return_null_when_email_invalid()
        {
            var sut = new UsersRepository(_fixture.DbContext);
            var user = await sut.FindByEmailAsync("invalid@email.com");
            user.Should().BeNull();
        }

        [Fact]
        public async Task CreateAsync_should_insert_new_user()
        {
            var sut = new UsersRepository(_fixture.DbContext);

            var user = new User("davide guida", "new_user@davideguida.com");

            var loadedUser = await sut.FindByEmailAsync(user.Email);
            loadedUser.Should().BeNull();

            await sut.CreateAsync(user);
            loadedUser = await sut.FindByEmailAsync(user.Email);
            loadedUser.Should().NotBeNull();
            loadedUser.Email.Should().Be(user.Email);
            loadedUser.Fullname.Should().Be(user.Fullname);
        }

        [Fact]
        public async Task CreateAsync_should_throw_if_email_already_exists()
        {
            var sut = new UsersRepository(_fixture.DbContext);

            var user1 = new User("davide guida", "existing_email@davideguida.com");
            await sut.CreateAsync(user1);

            var user2 = new User("davide guida", "existing_email@davideguida.com");
            var ex = await Assert.ThrowsAsync<MongoDB.Driver.MongoWriteException>(async () => await sut.CreateAsync(user2));
            ex.Message.Contains(user1.Email);
        }
    }
}
