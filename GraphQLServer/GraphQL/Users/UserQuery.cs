using GraphQLServer.GraphQL.Common;  
  
namespace GraphQLServer.GraphQL.Users;  
  
// UserQuery 확장  
[ExtendObjectType(typeof(Query))]  
public class UserQuery  
{  
    public async Task<IEnumerable<User>> GetUsers([Service] UserRepository userRepository)
    {
        return await userRepository.GetUsersAsync();
    }

    [GraphQLName("userByName")]
    public async Task<User?> GetUserByName(string name, [Service] UserRepository userRepository)
    {
        return await userRepository.GetUserByNameAsync(name);
    }
}  