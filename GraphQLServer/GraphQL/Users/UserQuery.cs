using GraphQLServer.GraphQL.Common;  
  
namespace GraphQLServer.GraphQL.Users;  
  
// UserQuery 확장  
[ExtendObjectType(typeof(Query))]  
public class UserQuery  
{  
    public IEnumerable<User> GetUsers()  
    {  
        return new List<User> {  
            new User { Id = 1, Name = "A", Desc = "THE USER A" },  
            new User { Id = 2, Name = "B", Desc = "THE USER B" }  
        };  
    }  
}  