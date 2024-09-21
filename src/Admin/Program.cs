using Application.Usecase;
using Domain.Repositories;
using Infrastructure.DAL;
using Infrastructure.Extensions;
using Infrastructure.Middleware;
using Infrastructure.Seed;
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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region Configurations
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));
#endregion
#region Db Connection
builder.Services.AddSingleton<AppDbContext>();
builder.Services.AddSingleton<Database>();
builder.Services.AddFluentMigratorService(builder.Configuration);
#endregion
#region Service Registry
#region Repository
builder.Services.AddScoped<IJobRepository, JobRepository>();
#endregion
#region Service
builder.Services.AddScoped<IJobService, JobService>();
#endregion
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

app.Migrate();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<GlobalExceptionHandler>();
app.MapControllers();
app.Run();