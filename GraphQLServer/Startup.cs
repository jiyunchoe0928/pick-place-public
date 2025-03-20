using GraphQLServer.GraphQL.Common;
using GraphQLServer.GraphQL.Users;
using GraphQLServer.GraphQL.Places;
using MongoDB.Driver;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// MongoDB 연결 설정
var mongoDbSettings = builder.Configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();

if (mongoDbSettings == null)
{
    throw new InvalidOperationException("MongoDBSettings configuration is missing or invalid.");
}

// MongoDB 클라이언트 등록
builder.Services.AddSingleton<IMongoClient>(s =>
{
    return new MongoClient(mongoDbSettings.ConnectionString);
});

// UserRepository 등록
builder.Services.AddSingleton<UserRepository>(s =>
{
    var mongoClient = s.GetRequiredService<IMongoClient>();
    return new UserRepository(mongoClient, mongoDbSettings.DatabaseName);
});

// Redis 연결 설정
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    string redisConnectionString = builder.Configuration.GetConnectionString("Redis") ?? "localhost";
    return ConnectionMultiplexer.Connect(redisConnectionString);
});


// ExternalUrlStrings 구성 추가
builder.Services.Configure<ExternalUrlStrings>(builder.Configuration.GetSection("ExternalUrlStrings"));


// HttpClient 등록 (ExternalPlaceClient에서 사용)
builder.Services.AddHttpClient<ExternalPlaceClient>();


// PlaceService 등록
builder.Services.AddScoped<PlaceService>(); // 또는 AddTransient, AddSingleton 상황에 따라 선택

// 로깅 서비스 추가
builder.Services.AddLogging(); 

builder.Services.AddSingleton(sp =>
{
    var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
    return loggerFactory.CreateLogger("Global");
});

// GraphQL 서비스 등록
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddTypeExtension<UserQuery>()
    .AddTypeExtension<PlaceQuery>()
    .AddTypeExtension<UserMutations>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// GraphQL 미들웨어 등록
app.MapGraphQL();

app.UseCors(); // CORS 미들웨어 추가

// 서버 실행
app.Run();

// MongoDB 설정을 담는 클래스
public class MongoDBSettings
{
    public required string ConnectionString { get; set; }
    public required string DatabaseName { get; set; }
}

// ExternalUrlStrings 설정 클래스 추가
public class ExternalUrlStrings
{
    public required string Place { get; set; }
}