var builder = WebApplication.CreateBuilder(args);


builder.Services.AddApplicationInsightsTelemetry(new Microsoft.ApplicationInsights.AspNetCore.Extensions.ApplicationInsightsServiceOptions
{
    ConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]
});

var app = builder.Build();

app.MapGet("/", () => "Hello World, CI/CD is a GO <3 !!");

app.Run();
