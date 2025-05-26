using DAL;
using Serilog;
using Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//var app = builder.Build();
var build = new ConfigurationBuilder();

build.AddJsonFile("appsettings.json");
var configuration = build.Build();

Log.Logger = new LoggerConfiguration()
               .ReadFrom.Configuration(configuration)
               .CreateLogger();

builder.Services.AddSingleton(Log.Logger);
builder.Services.Configure<DbContextSettings>(configuration);
builder.Services.AddService(configuration);
builder.Services.AddHttpClient();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy",
        builder => builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .SetIsOriginAllowed((hosts) => true));
});
builder.Services.AddControllers().AddNewtonsoftJson(options =>
                          options.SerializerSettings.
                          ReferenceLoopHandling =
                          Newtonsoft.Json.ReferenceLoopHandling.Ignore);

var app = builder.Build();

app.UseRouting();
app.UseCors("CORSPolicy");
app.UseAuthorization();
app.MapControllers();

app.Run();
