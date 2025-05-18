using EventManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

#if false
// Reset database by dropping and recreating tables
try
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        Console.WriteLine("Attempting to reset database tables...");

        // Drop all tables
        await dbContext.Database.ExecuteSqlRawAsync("DROP SCHEMA public CASCADE; CREATE SCHEMA public;");
        Console.WriteLine("All tables dropped successfully!");

        // Recreate tables based on models
        await dbContext.Database.EnsureCreatedAsync();
        Console.WriteLine("New tables created successfully!");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Failed to reset database tables: {ex.Message}");
    Console.WriteLine($"Inner exception: {ex.InnerException?.Message}");
    return;
}
#endif
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
