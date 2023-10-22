using Microsoft.Extensions.Options;
using MongoDB.Driver;
using src.Models;

namespace src.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserService(IOptions<UserDatabaseConfiguration> userConfiguration)
        {
            var mongoClient = new MongoClient(userConfiguration.Value.ConnectionString);
            var database = mongoClient.GetDatabase(userConfiguration.Value.DatabaseName);

            _userCollection = database.GetCollection<User>(userConfiguration.Value.CollectionName);
        }

        public async Task<List<User>> GetAsync()
        => await _userCollection.Find(user => true).ToListAsync();

        public async Task<User> GetAsync(string id)
        => await _userCollection.Find(user => user.Id == id).FirstOrDefaultAsync();

        public async Task CreateUser(User user)
        => await _userCollection.InsertOneAsync(user);

        public async Task ReplaceAsync(string id, User user)
        => await _userCollection.ReplaceOneAsync(user => user.Id == id, user);

        public async Task DeleteAsync(string id)
        => await _userCollection.DeleteOneAsync(user => user.Id == id);
    }
}
