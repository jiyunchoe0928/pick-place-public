using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using GraphQLServer.GraphQL.Places.External;
using Microsoft.Extensions.Options;

public class RequestBody
{
    public required string Message { get; set; }
}

public class ExternalPlaceClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ExternalPlaceClient> _logger; 
    private readonly string _placeUrl;

    public ExternalPlaceClient(HttpClient httpClient, ILogger<ExternalPlaceClient> logger, IOptions<ExternalUrlStrings> externalUrlStrings)
    {
        _httpClient = httpClient;
        _placeUrl = externalUrlStrings.Value.Place;
        _httpClient.BaseAddress = new Uri(_placeUrl);

        _logger = logger;
    }

    public async Task<ExternalPlaceDto?> GetPlaceDataAsync(string message)
    {
        try
        {
            var requestBody = new { message, language = "ja" };

            var jsonContent = JsonContent.Create(requestBody);
            var response = await _httpClient.PostAsync("/dev/place/find", jsonContent);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
           

            return await response.Content.ReadFromJsonAsync<ExternalPlaceDto>();

        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error calling external API: {ex.Message}");

            return null;
        }
    }

    public async Task<List<ExternalPlaceDto>?> GetInitPlacesDataAsync()
    {
        try
        {

            var response = await _httpClient.GetAsync("/dev/places/init");

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
           

            return await response.Content.ReadFromJsonAsync<List<ExternalPlaceDto>>();

        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error calling external API: {ex.Message}");

            return null;
        }
    }
}