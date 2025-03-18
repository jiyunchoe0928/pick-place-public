using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GraphQLServer.GraphQL.Users;  
  
// User 엔티티 정의  
public class User  
{  
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    [BsonElement("name")]
    public required string Name { get; set; }

    [BsonElement("desc")]
    public required string Desc { get; set; }
}  