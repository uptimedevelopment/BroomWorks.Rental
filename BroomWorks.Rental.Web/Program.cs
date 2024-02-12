using BroomWorks.Rental.Business;
using BroomWorks.Rental.Data;
using BroomWorks.Rental.Domain;
using BroomWorks.Rental.Web.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options => options
    .UseSqlServer(builder.Configuration.GetConnectionString("Default"))
    .EnableDetailedErrors(builder.Environment.IsDevelopment())
    .EnableSensitiveDataLogging(builder.Environment.IsDevelopment()));

builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>();

builder.Services.AddApplicationInsightsTelemetry(new Microsoft.ApplicationInsights.AspNetCore.Extensions.ApplicationInsightsServiceOptions
{
    ConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]
});

builder.Services.AddScoped<IApplicationContext, ApplicationContext>();
builder.Services.RegisterRepositories();
builder.Services.RegisterServices();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();
app.MapHealthChecks("/healthz").AllowAnonymous();
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

app.Run();
