using GraphQLServer.GraphQL.Common;  
  
namespace GraphQLServer.GraphQL.Users;  
  
[ExtendObjectType(typeof(Mutation))]  
public class UserMutations  
{  
    public bool AddUser(string name, string desc)  
    {  
        // 실제 구현은 DB 등을 활용하여 저장  
        return true; // 단순히 성공 여부를 반환  
    }  
}  