namespace MongoDbIntegrationSample.Models
{
    public class User
    {
        public User(string fullname, string email)
        {
            Fullname = fullname;
            Email = email;
        }

        public string Fullname { get; }
        public string Email { get; }
    }
}
