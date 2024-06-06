using IOXFleetServicesAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//*********************
// Swagger
//*********************
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "IOX Fleet Service API",
        Description = "An API for managing Vehicles, Accounts & Licenses."
    });
});

//*********************
// Database Context
//*********************
builder.Services.AddDbContext<LocalDatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("databaseContext"));
});

//*********************
// Services
//*********************
//builder.Services.AddScoped<IAccountService, AccountService>();
//builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(Program).Assembly));

//*********************
// Cors
//*********************
builder.Services.AddCors(p => p.AddDefaultPolicy(builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

//*********************
// Logging
//*********************
Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.WithProperty("ApplicationName", "IOX Fleet Service API")
            .Enrich.WithProperty("Environment", "Production")

            //Client Info
            .Enrich.WithProcessId()
            .Enrich.WithProcessName()
            .Enrich.WithThreadId()
            .Enrich.WithClientIp()
            .Enrich.WithCorrelationId()
            .Enrich.WithRequestHeader("Header")

            //Console
            .WriteTo.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level} {ClientIp} {ClientAgent}] {Message:lj}{NewLine}{Exception}")

            //File
            .WriteTo.File("logs/logs-.txt", rollingInterval: RollingInterval.Day, outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level} {ClientIp} {ClientAgent}] {Message:lj}{NewLine}{Exception}")

            //File josn - all logs
            .WriteTo.File(new Serilog.Formatting.Compact.CompactJsonFormatter(), "logs/logs-.json", rollingInterval: RollingInterval.Day)

          .CreateLogger();

builder.Logging.AddSerilog(Log.Logger);
builder.Services.AddSingleton<Serilog.ILogger>(Log.Logger);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<LocalDatabaseContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

Log.Information("API Service Started");
app.Run();

