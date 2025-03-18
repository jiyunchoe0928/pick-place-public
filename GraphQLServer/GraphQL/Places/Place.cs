namespace GraphQLServer.GraphQL.Places;  
  
// Place 엔티티 정의  
public class Place  
{  
    public int Id { get; set; }  
    public required string Name { get; set; }  
    public required string Location { get; set; }  
}