using GraphQLServer.GraphQL.Common;  
using GraphQLServer.GraphQL.Users;  
using GraphQLServer.GraphQL.Places;  
  
var builder = WebApplication.CreateBuilder(args);  
  
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