using Mongemini.Bus.RabbitMq;
using Mongemini.Persistence.Implementations.Extensions;
using Mongemini.Service.Application;
using Mongemini.Service.Domain;
using Mongemini.Service.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// logger
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", options =>
{
    options
    .AllowAnyHeader()
    .AllowAnyMethod()
    .SetIsOriginAllowed(host => true);
}));

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddDomain();
builder.Services.AddApplication();
builder.Services.AddBus(builder.Configuration);

var app = builder.Build();

app.MigrateDatabasesAsync(CancellationToken.None);
app.UseEventBus();

app.UseCors("CorsPolicy");

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();

