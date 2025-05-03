using Microsoft.EntityFrameworkCore;
using STZ.LocalizationService.Access.DbContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ResourceServiceContext>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Execute database migrations
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ResourceServiceContext>();
    await dbContext.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();
app.Run();