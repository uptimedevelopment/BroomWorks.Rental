var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => "Hello World, CI/CD is a GO <3 !!");

app.Run();
