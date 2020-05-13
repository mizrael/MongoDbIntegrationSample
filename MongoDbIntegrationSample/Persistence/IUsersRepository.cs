using System.Threading.Tasks;
using MongoDbIntegrationSample.Models;

namespace MongoDbIntegrationSample.Persistence
{
    public interface IUsersRepository
    {
        Task<User> FindByEmailAsync(string email);
    }
}