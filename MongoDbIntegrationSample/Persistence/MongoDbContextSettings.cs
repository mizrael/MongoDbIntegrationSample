namespace MongoDbIntegrationSample.Persistence
{
    public class MongoDbContextSettings
    {
        public MongoDbContextSettings(string connectionString, string databaseName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
        }

        public string ConnectionString { get; }
        public string DatabaseName { get; }
    }
}