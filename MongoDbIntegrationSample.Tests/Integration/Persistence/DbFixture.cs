using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDbIntegrationSample.Persistence;

namespace MongoDbIntegrationSample.Tests.Integration.Persistence
{
    public class DbFixture : IDisposable
    {
        public DbFixture()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var connString = config.GetConnectionString("db");
            var dbName = $"test_db_{Guid.NewGuid()}";

            this.DbContextSettings = new MongoDbContextSettings(connString, dbName);
            this.DbContext = new MongoDbContext(this.DbContextSettings);
        }

        public MongoDbContextSettings DbContextSettings { get; }
        public MongoDbContext DbContext { get; }

        public void Dispose()
        {
            var client = new MongoClient(this.DbContextSettings.ConnectionString);
            client.DropDatabase(this.DbContextSettings.DatabaseName);
        }
    }
}