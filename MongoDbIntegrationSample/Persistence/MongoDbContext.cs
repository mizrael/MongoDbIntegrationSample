using System;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDbIntegrationSample.Models;

namespace MongoDbIntegrationSample.Persistence
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;

        static MongoDbContext()
        {
            BsonClassMap.RegisterClassMap<User>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
        }

        public MongoDbContext(MongoDbContextSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);
        }

        public async Task<IMongoCollection<User>> GetUsersAsync()
        {
            var coll = _database.GetCollection<User>("users");

            var indexes = await coll.Indexes.ListAsync();
            var hasIndexes = await indexes.AnyAsync();
            if (!hasIndexes)
            {
                var indexBuilder = Builders<User>.IndexKeys;
                var keys = indexBuilder.Ascending(u => u.Email);
                var options = new CreateIndexOptions
                {
                    Background = true,
                    Unique = true
                };
                var indexModel = new CreateIndexModel<User>(keys, options);
               await coll.Indexes.CreateOneAsync(indexModel);
            }

            return coll;
        }
    }
}