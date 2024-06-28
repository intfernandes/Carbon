using Api;
using Api.Common.Api;
using Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

builder.AddConfiguration();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
    {
        app.ConfigureDevEnvironment();
    }

app.UseCors(ApiConfiguration.CorsPolicyName);

app.MapEndpoints();

app.MapDefaultEndpoints();

app.Run();

