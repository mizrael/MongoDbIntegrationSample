using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDbIntegrationSample.Models;

namespace MongoDbIntegrationSample.Persistence
{
    public interface IMongoDbContext
    {
        Task<IMongoCollection<User>> GetUsersAsync();
    }
}