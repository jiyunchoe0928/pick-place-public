using GraphQLServer.GraphQL.Common;
using GraphQLServer.GraphQL.Users;
using GraphQLServer.GraphQL.Places;
using MongoDB.Driver;

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

// 로깅 서비스 추가
builder.Services.AddLogging(); 

// GraphQL 서비스 등록
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()         // Mutation 추가
    .AddTypeExtension<UserQuery>()
    .AddTypeExtension<PlaceQuery>()
    .AddTypeExtension<UserMutations>();  // Mutation 확장 추가

var app = builder.Build();

// GraphQL 미들웨어 등록
app.MapGraphQL();

// 서버 실행
app.Run();

// MongoDB 설정을 담는 클래스
public class MongoDBSettings
{
    public required string ConnectionString { get; set; }
    public required string DatabaseName { get; set; }
}