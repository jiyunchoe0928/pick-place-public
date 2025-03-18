namespace GraphQLServer.GraphQL.Users;  
  
// User 엔티티 정의  
public class User  
{  
    public int Id { get; set; }  
    public required string Desc { get; set; }  
    public required string Name { get; set; }  
}  