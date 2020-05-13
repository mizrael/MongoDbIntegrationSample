using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDbIntegrationSample.Models;

namespace MongoDbIntegrationSample.Persistence
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IMongoDbContext _dbContext;

        public UsersRepository(IMongoDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(email));
            var users = await _dbContext.GetUsersAsync();
            var cursor = await users.FindAsync(u => u.Email == email);
            return await cursor.FirstOrDefaultAsync();
        }

        public async Task CreateAsync(User user)
        {
            if (user == null) 
                throw new ArgumentNullException(nameof(user));
            var users = await _dbContext.GetUsersAsync();
            await users.InsertOneAsync(user);
        }
    }
}
