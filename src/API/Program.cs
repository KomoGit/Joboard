using Application.Usecase;
using Domain.Repositories;
using Infrastructure.DAL;
using Infrastructure.Middleware;
using Job.Module.Queries;
using Job.Module.Services;
using Serilog;
using SharedKernel.Domain.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
#region Serilog
var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
});
var logger = loggerFactory.CreateLogger<Program>();
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();
#endregion
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region Configurations
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection($"{typeof(DatabaseSettings).Name}Readonly"));
#endregion
#region Db Connection
builder.Services.AddSingleton<AppDbContext>();
#endregion
#region Service Registry
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IJobQueries, JobQueries>();
#endregion
#region Global Exception Handler
builder.Services.AddLogging();
builder.Services.AddTransient<GlobalExceptionHandler>();
#endregion

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<GlobalExceptionHandler>();
app.MapControllers();
app.Run();