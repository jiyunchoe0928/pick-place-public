// Services/PlaceService.cs
using GraphQLServer.GraphQL.Places;
using GraphQLServer.GraphQL.Places.External;
using StackExchange.Redis;
using System.Text.Json;
using System.Threading.Tasks;


public class PlaceService
{
    private readonly ILogger _logger;
    private readonly IConnectionMultiplexer _redis;
    private readonly ExternalPlaceClient _externalPlaceClient;

    public PlaceService(ILogger<PlaceService> logger, IConnectionMultiplexer redis, ExternalPlaceClient externalPlaceClient)
    {
        _logger = logger;
        _redis = redis;
        _externalPlaceClient = externalPlaceClient;
    }

    public async Task<List<Place>?> GetInitPlaceAsync()
    {
        IDatabase db = _redis.GetDatabase();

        string cacheKey = $"place:init";
        string? cachedPlace = await db.StringGetAsync(cacheKey);

        // if (!string.IsNullOrEmpty(cachedPlace))
        // {
        //     return JsonSerializer.Deserialize<List<Place>>(cachedPlace);
        // }

        List<ExternalPlaceDto>? externalPlaceDtos = await _externalPlaceClient.GetInitPlacesDataAsync();
        if (externalPlaceDtos == null || externalPlaceDtos.Count == 0)
        {
            return null;
        }

        List<Place> places = externalPlaceDtos.Select(MapDtoToEntity).ToList();


        await db.StringSetAsync(cacheKey, JsonSerializer.Serialize(places), TimeSpan.FromMinutes(30));

        return places;
    }

    public async Task<Place?> GetPlaceByMessageAsync(string message)
    {
        IDatabase db = _redis.GetDatabase();

        string? cachedKeyword = await db.StringGetAsync($"place:key:{message}");
        _logger.LogInformation($"[PlaceService] cachedKeyword: {cachedKeyword}");

        if (!string.IsNullOrEmpty(cachedKeyword))
        {
          string? cachedPlace = await db.StringGetAsync($"place:{cachedKeyword}");

          if (!string.IsNullOrEmpty(cachedPlace))
          {
              return JsonSerializer.Deserialize<Place>(cachedPlace);
          }
        }

        ExternalPlaceDto? externalPlaceDto = await _externalPlaceClient.GetPlaceDataAsync(message);
        if (externalPlaceDto == null)
        {
            return null;
        }

        Place place = MapDtoToEntity(externalPlaceDto);

        string keywordCacheKey = $"place:key:{message}";
        string placeCacheKey = $"place:{cachedKeyword}";

        await db.StringSetAsync(keywordCacheKey, place.Key, TimeSpan.FromMinutes(30));

        TimeSpan placeCacheTime = place.CurrentState == "raw" ? TimeSpan.FromSeconds(5) : TimeSpan.FromMinutes(30);
        await db.StringSetAsync(placeCacheKey, JsonSerializer.Serialize(place), placeCacheTime);

        return place;
    }

    private Place MapDtoToEntity(ExternalPlaceDto externalPlace)
    {
      return new Place
      {
          Key = externalPlace.Key ?? "DefaultKey",
          Timestamp = externalPlace.Timestamp ?? DateTime.UtcNow.ToString("O"),
          OverallSummary = externalPlace.OverallSummary ?? string.Empty,
          SearchUri = externalPlace.SearchUri ?? string.Empty,
          UpdatedTime = externalPlace.UpdatedTime ?? DateTime.UtcNow.ToString("O"),
          CurrentState = externalPlace.CurrentState ?? "Unknown",
          Summaries = externalPlace.Summaries?.Select(externalPlaceSummary => new Summary{
            Name = externalPlaceSummary.Name ?? string.Empty,
            SummaryDetail = new PlaceSummary()
            {
              Positive = externalPlaceSummary?.SummaryDetail?.Positive ?? string.Empty,
              Neutral = externalPlaceSummary?.SummaryDetail?.Neutral ?? string.Empty,
              Negative = externalPlaceSummary?.SummaryDetail?.Negative ?? string.Empty,
            }            
          }).ToList() ?? new List<Summary>(),
          Places = externalPlace.Places?.Select(externalPlaceDetail => new PlaceDetail
          {
              Id = externalPlaceDetail.Id ?? string.Empty,
              DisplayName = new DisplayName()
              {
                LanguageCode = externalPlaceDetail.DisplayName?.LanguageCode ?? string.Empty,
                Text = externalPlaceDetail.DisplayName?.Text ?? string.Empty,
              },
              RegularOpeningHours = new RegularOpeningHours()
              {
                WeekdayDescriptions = externalPlaceDetail.RegularOpeningHours?.WeekdayDescriptions ?? new List<string>(), 
              },
              AdrFormatAddress = externalPlaceDetail.AdrFormatAddress ?? string.Empty,
              BusinessStatus = externalPlaceDetail.BusinessStatus ?? string.Empty,
              FormattedAddress = externalPlaceDetail.FormattedAddress ?? string.Empty,
              GoogleMapsUri = externalPlaceDetail.GoogleMapsUri ?? string.Empty,
              IconBackgroundColor = externalPlaceDetail.IconBackgroundColor ?? string.Empty,
              IconMaskBaseUri = externalPlaceDetail.IconMaskBaseUri ?? string.Empty,
              Name = externalPlaceDetail.Name ?? string.Empty,
              NationalPhoneNumber = externalPlaceDetail.NationalPhoneNumber ?? string.Empty,
              PrimaryType = externalPlaceDetail.PrimaryType ?? string.Empty,
              ShortFormattedAddress = externalPlaceDetail.ShortFormattedAddress ?? string.Empty,
              GoogleMapsLinks = new GoogleMapsLinks()
              {
                  DirectionsUri = externalPlaceDetail.GoogleMapsLinks?.DirectionsUri ?? string.Empty,
                  PhotosUri = externalPlaceDetail.GoogleMapsLinks?.PhotosUri ?? string.Empty,
                  PlaceUri = externalPlaceDetail.GoogleMapsLinks?.PlaceUri ?? string.Empty,
                  ReviewsUri = externalPlaceDetail.GoogleMapsLinks?.ReviewsUri ?? string.Empty,
                  WriteAReviewUri = externalPlaceDetail.GoogleMapsLinks?.WriteAReviewUri ?? string.Empty,
              },
              Location = new MapPosition()
              {
                  Latitude = externalPlaceDetail.Location?.Latitude,
                  Longitude = externalPlaceDetail.Location?.Longitude,
              },
              ContextualContents = new ContextualContents()
              {
                  Reviews = externalPlaceDetail.ContextualContents?.Reviews?.Select(review => new Review()
                  {
                      AuthorAttribution = new AuthorAttribution()
                      {
                          DisplayName = review.AuthorAttribution?.DisplayName ?? string.Empty,
                          PhotoUri = review.AuthorAttribution?.PhotoUri ?? string.Empty,
                          Uri = review.AuthorAttribution?.Uri ?? string.Empty,
                      },
                      FlagContentUri = review.FlagContentUri ?? string.Empty,
                      GoogleMapsUri = review.GoogleMapsUri ?? string.Empty,
                      Name = review.Name ?? string.Empty,
                      OriginalText = new DisplayName()
                      {
                          LanguageCode = review.OriginalText?.LanguageCode ?? string.Empty,
                          Text = review.OriginalText?.Text ?? string.Empty,
                      },
                      PublishTime = review.PublishTime ?? string.Empty,
                      Rating = review.Rating ?? 0,
                      RelativePublishTimeDescription = review.RelativePublishTimeDescription ?? string.Empty,
                      Text = new DisplayName()
                      {
                          LanguageCode = review.Text?.LanguageCode ?? string.Empty,
                          Text = review.Text?.Text ?? string.Empty,
                      }
                  }).ToList() ?? new List<Review>()
              },
              AccessibilityOptions = new AccessibilityOptions()
              {
                  WheelchairAccessibleEntrance = externalPlaceDetail.AccessibilityOptions?.WheelchairAccessibleEntrance,
                  WheelchairAccessibleParking = externalPlaceDetail.AccessibilityOptions?.WheelchairAccessibleParking,
              },
              AddressDescriptor = new AddressDescriptor()
              {
                  Areas = externalPlaceDetail.AddressDescriptor?.Areas?.Select(area => new Area()
                  {
                      Containment = area.Containment ?? string.Empty,
                      DisplayName = new DisplayName()
                      {
                          LanguageCode = area.DisplayName?.LanguageCode ?? string.Empty,
                          Text = area.DisplayName?.Text ?? string.Empty,
                      },
                      Name = area.Name ?? string.Empty,
                      PlaceId = area.PlaceId ?? string.Empty,
                  }).ToList() ?? new List<Area>(),
                  Landmarks = externalPlaceDetail.AddressDescriptor?.Landmarks?.Select(landmark => new Landmark()
                  {
                      DisplayName = new DisplayName()
                      {
                          LanguageCode = landmark.DisplayName?.LanguageCode ?? string.Empty,
                          Text = landmark.DisplayName?.Text ?? string.Empty,
                      },
                      Name = landmark.Name ?? string.Empty,
                      PlaceId = landmark.PlaceId ?? string.Empty,
                      SpatialRelationship = landmark.SpatialRelationship ?? string.Empty,
                      StraightLineDistanceMeters = landmark.StraightLineDistanceMeters ?? 0,
                      Types = landmark.Types ?? new List<string>(),
                  }).ToList() ?? new List<Landmark>(),
              },
              AddressComponents = externalPlaceDetail.AddressComponents?.Select(addressComponent => new AddressComponent()
              {
                  LanguageCode = addressComponent.LanguageCode ?? string.Empty,
                  LongText = addressComponent.LongText ?? string.Empty,
                  ShortText = addressComponent.ShortText ?? string.Empty,
                  Types = addressComponent.Types ?? new List<string>(),
              }).ToList() ?? new List<AddressComponent>(),
              Types = externalPlaceDetail.Types ?? new List<string>(),
              Photos = externalPlaceDetail.Photos?.Select(photo => new Photo()
              {
                  AuthorAttributions = photo.AuthorAttributions?.Select(authorAttribution => new AuthorAttribution()
                  {
                      DisplayName = authorAttribution.DisplayName ?? string.Empty,
                      PhotoUri = authorAttribution.PhotoUri ?? string.Empty,
                      Uri = authorAttribution.Uri ?? string.Empty,
                  }).ToList() ?? new List<AuthorAttribution>(),
                  FlagContentUri = photo.FlagContentUri ?? string.Empty,
                  GoogleMapsUri = photo.GoogleMapsUri ?? string.Empty,
                  HeightPx = photo.HeightPx ?? 0,
                  Name = photo.Name ?? string.Empty,
                  WidthPx = photo.WidthPx ?? 0,
              }).ToList() ?? new List<Photo>(),
              PlusCode = new PlusCode()
              {
                  CompoundCode = externalPlaceDetail.PlusCode?.CompoundCode ?? string.Empty,
                  GlobalCode = externalPlaceDetail.PlusCode?.GlobalCode ?? string.Empty,
              },
              Viewport = new Viewport()
              {
                  High = new MapPosition()
                  {
                      Latitude = externalPlaceDetail.Viewport?.High?.Latitude,
                      Longitude = externalPlaceDetail.Viewport?.High?.Longitude,
                  },
                  Low = new MapPosition()
                  {
                      Latitude = externalPlaceDetail.Viewport?.Low?.Latitude,
                      Longitude = externalPlaceDetail.Viewport?.Low?.Longitude,
                  }
              },
              UserRatingCount = externalPlaceDetail.UserRatingCount ?? 0,
              Rating = externalPlaceDetail.Rating ?? 0,
          }).ToList() ?? new List<PlaceDetail>(),
      };
    }

    public async Task ClearCacheAsync(string keyword)
    {
      IDatabase db = _redis.GetDatabase();
      string cacheKey = $"place:{keyword}";
      await db.KeyDeleteAsync(cacheKey);
    }
}