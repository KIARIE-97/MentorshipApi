using Mentorship.Api.Data;
using Mentorship.API.Middleware;
using Mentorship.API.Services;
using Mentorship.Application;
using Mentorship.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

//add layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Mentorship API",
        Version = "v1",
        Description = "API for managing mentorship programs",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Support Team",
            Email = "support@mentorship.com"
        }
    });
     // Add Idempotency-Key header to Swagger
    c.AddSecurityDefinition("IdempotencyKey", new OpenApiSecurityScheme
    {
        Name = "Idempotency-Key",
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Description = "Idempotency key to prevent duplicate requests"
    });
    
    // Make it required for POST/PUT/PATCH operations
    c.OperationFilter<IdempotencyKeyOperationFilter>();
});


// Create this filter
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
    config.ApiVersionReader = new UrlSegmentApiVersionReader();
});



// Add this for Swagger to understand versions
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// Configure DbContext with explicit migrations assembly
builder.Services.AddDbContext<AppDbContext>((options) =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions =>
        {
            npgsqlOptions.MigrationsAssembly("Mentorship.Infrastructure");
            // Optional: Set the schema
            // npgsqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "public");
        });
});

//redis config >>> idempotemcy
var redisConfig = new ConfigurationOptions
{
    // Your Redis Cloud endpoint
    EndPoints = { { "redis-17951.c261.us-east-1-4.ec2.cloud.redislabs.com", 17951 } },
    
    // Authentication
    User = builder.Configuration["Redis:Username"] ?? "default",
    Password = builder.Configuration["Redis:Password"],
    
    // Connection optimization
    AbortOnConnectFail = false,      // Don't crash if Redis is temporarily down
    ConnectRetry = 3,                 // Retry 3 times on failure
    ConnectTimeout = 5000,            // 5 second timeout
    SyncTimeout = 5000,               // 5 second sync timeout
    AsyncTimeout = 5000,              // 5 second async timeout
    
    // Keep connection alive
    KeepAlive = 5,                    // Send keep-alive every 5 seconds
    
    // Performance
    Ssl = false,                      // Your endpoint doesn't use SSL (check if needed)
    
    // Default database (0-15, Redis Cloud usually uses 0)
    DefaultDatabase = 0,
    
    // Configure for high availability
    ReconnectRetryPolicy = new LinearRetry(1000)  // Retry every second
};

builder.Services.AddSingleton<IConnectionMultiplexer>(sp => 
    ConnectionMultiplexer.Connect(redisConfig));

    //scoped create a new instance every new http request eg.db contes
builder.Services.AddSingleton<IIdempotencyService, RedisIdempotencyService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<IdempotencyMiddleware>();
app.MapControllers();

app.Run();


