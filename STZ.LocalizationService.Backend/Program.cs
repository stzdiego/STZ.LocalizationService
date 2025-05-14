using Microsoft.EntityFrameworkCore;
using STZ.Backend.Configuration;
using STZ.LocalizationService.Access.DbContext;

var builder = WebApplication.CreateBuilder(args);

// STZFramework configuration
builder.Services.AddSTZBackendServices(builder.Configuration);

// DbContext configuration
builder.Services.AddDbContext<ResourceServiceContext>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Execute database migrations
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ResourceServiceContext>();
    await dbContext.Database.MigrateAsync(); // Apply migrations
    await scope.ServiceProvider.ExecuteSeedersAsync(); // Execute seeders
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();
app.Run();