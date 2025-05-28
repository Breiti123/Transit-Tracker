using TransitTracker.TrafficAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ITrafficService, MockTrafficService>();

var app = builder.Build();
app.MapControllers();

app.Run();
