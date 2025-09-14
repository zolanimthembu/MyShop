using Microsoft.EntityFrameworkCore;
using MyShop.Server.Data;
using MyShop.Server.Repositories;
using MyShop.Server.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog (console) - optional but helpful
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers();

// EF Core - SQL Server
var connStr = builder.Configuration.GetConnectionString("DefaultConnection")
              ?? "Server=host.docker.internal;Database=ShopDb;User Id=myshopuser;Password=myshopuser;Encrypt=False;TrustServerCertificate=True;";

builder.Services.AddDbContext<ShopDbContext>(options =>
    options.UseSqlServer(connStr, sqlOptions =>
    {
        // Optional: configure retry on failure for transient faults
        sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null);
    }));

// DI: repository + service
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

// AutoMapper (if you want to use it later)
builder.Services.AddAutoMapper(typeof(Program));

// CORS for local dev frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalDev", policy =>
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .WithOrigins("http://localhost:3000"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply migrations and seed (safe for dev)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var db = services.GetRequiredService<ShopDbContext>();
        // Apply migrations
        db.Database.Migrate();

        // Seed data if needed
        await SeedData.EnsureSeedDataAsync(db);
    }
    catch (Exception ex)
    {
        Log.Fatal(ex, "An error occurred seeding the DB.");
        throw;
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors("LocalDev");

app.UseAuthorization();

app.MapControllers();

app.Run();
