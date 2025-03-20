using GraphQLServer.GraphQL.Users;
using MongoDB.Driver;

public class UserRepository
{
    private readonly IMongoCollection<User> _users;

    public UserRepository(IMongoClient mongoClient, string databaseName)
    {
        var database = mongoClient.GetDatabase(databaseName);
        _users = database.GetCollection<User>("Users");
    }

    public async Task<List<User>> GetUsersAsync() =>
        await _users.Find(_ => true).ToListAsync();

    public async Task<User> GetUserAsync(string id) =>
        await _users.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<User?> GetUserByNameAsync(string name) =>
        await _users.Find(x => x.Name == name).FirstOrDefaultAsync();

    public async Task CreateUserAsync(User newUser) =>
        await _users.InsertOneAsync(newUser);

    public async Task UpdateUserAsync(string id, User updatedUser) =>
        await _users.ReplaceOneAsync(x => x.Id == id, updatedUser);

    public async Task DeleteUserAsync(string id) =>
        await _users.DeleteOneAsync(x => x.Id == id);
}