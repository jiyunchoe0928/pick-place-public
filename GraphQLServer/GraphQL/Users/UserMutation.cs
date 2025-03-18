using GraphQLServer.GraphQL.Common;
using HotChocolate;
using HotChocolate.Types;

namespace GraphQLServer.GraphQL.Users;

[ExtendObjectType(typeof(Mutation))]
public class UserMutations
{
    [GraphQLName("addUser")]
    public async Task<User> AddUser(
        string name,
        string desc,
        [Service] UserRepository userRepository)
    {
        var user = new User
        {
            Name = name,
            Desc = desc
        };

        await userRepository.CreateUserAsync(user);
        return user;
    }
}