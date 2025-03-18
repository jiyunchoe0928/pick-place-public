using GraphQLServer.GraphQL.Common;  
  
namespace GraphQLServer.GraphQL.Places;  
  
// PlaceQuery 확장  
[ExtendObjectType(typeof(Query))]  
public class PlaceQuery  
{  
    public IEnumerable<Place> GetPlaces()  
    {  
        return new List<Place> {  
            new Place { Id = 1, Name = "Central Park", Location = "New York" },  
            new Place { Id = 2, Name = "Louvre Museum", Location = "Paris" }  
        };  
    }  
}  