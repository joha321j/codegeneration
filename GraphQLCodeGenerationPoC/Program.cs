using GraphQLCodeGenerationPoC.Models;
using HotChocolate.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddDocumentFromFile("schema.graphql")
    .BindRuntimeType<Query>();

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
    endpoints.MapGraphQL().WithOptions(new GraphQLServerOptions
    {
        AllowedGetOperations = AllowedGetOperations.Query
    }));

app.Run();
