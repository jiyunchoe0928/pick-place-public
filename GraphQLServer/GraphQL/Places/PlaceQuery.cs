using GraphQLServer.GraphQL.Common;  
  
namespace GraphQLServer.GraphQL.Places;  
  
[ExtendObjectType(typeof(Query))]  
public class PlaceQuery  
{  
    [GraphQLName("placeByMessage")]
    public async Task<Place?> GetPlaceByMessage(string message, [Service] PlaceService placeService)
    {
        return await placeService.GetPlaceByMessageAsync(message);
    }

    [GraphQLName("initPlaces")]
    public async Task<List<Place>?> GetInitPlaces([Service] PlaceService placeService)
    {
        return await placeService.GetInitPlaceAsync();
    }
}  