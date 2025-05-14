using Microsoft.EntityFrameworkCore.DynamicLinq;
using STZ.Backend.Bases;
using STZ.LocalizationService.Access.DbContext;
using STZ.Shared.Bases;
using STZ.Shared.Entities;

namespace STZ.LocalizationService.Backend.Seeders;

public class CultureSeeder : IDataSeeder
{
    public async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ResourceServiceContext>();
        var isAny = await context.Cultures.AnyAsync();

        if (!isAny)
        {
            await context.Cultures.AddRangeAsync(
                new Culture { Code = "en-US", Name = "English United States" },
                new Culture { Code = "es-CO", Name = "Español Colombia" }
            );
            await context.SaveChangesAsync();
        }
    }
}