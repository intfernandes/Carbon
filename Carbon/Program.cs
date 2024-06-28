var builder = DistributedApplication.CreateBuilder(args);

var Api = builder.AddProject<Projects.Api>("Api");

// builder.AddProject<Projects.Web>("Web")
//     .WithExternalHttpEndpoints()
//     .WithReference(Api);


builder.Build().Run();
