using Microsoft.EntityFrameworkCore;
using STZ.Backend.Bases;
using STZ.LocalizationService.Access.DbContext;
using STZ.Shared.Entities;

namespace STZ.LocalizationService.Backend.Seeders;

public class ResourceSeeder : IDataSeeder
{
    public async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ResourceServiceContext>();
        var isAny = await context.Resources.AnyAsync();

        if (!isAny)
        {
            var cultures = await context.Cultures.ToDictionaryAsync(c => c.Code); // Code: "es-CO", "en-US"
            
            foreach (var (code, translations) in GetResources())
            {
                var resource = await context.Resources.FirstOrDefaultAsync(r => r.Code == code);
                if (resource == null)
                {
                    resource = new Resource { Code = code };
                    context.Resources.Add(resource);
                    await context.SaveChangesAsync(); // Guarda para obtener el ID
                }

                foreach (var (cultureCode, text) in translations)
                {
                    var culture = cultures[cultureCode];
                    var exists = await context.ResourceCultures
                        .AnyAsync(rc => rc.ResourceId.Equals(resource.Id) && rc.CultureId.Equals(culture.Id));

                    if (!exists)
                    {
                        context.ResourceCultures.Add(new ResourceCulture<Guid, Guid>
                        {
                            ResourceId = resource.Id,
                            CultureId = culture.Id,
                            Text = text
                        });
                    }
                }
            }

            await context.SaveChangesAsync();
            
            await context.SaveChangesAsync();
        }
    }

    private Dictionary<string, Dictionary<string, string>> GetResources()
    {
        return new Dictionary<string, Dictionary<string, string>>
        {
            ["App.Login"] = new() { ["es-CO"] = "Iniciar sesión", ["en-US"] = "Login" },
            ["App.Logout"] = new() { ["es-CO"] = "Cerrar sesión", ["en-US"] = "Logout" },
            ["Login.NoAuthorized.Title"] = new() { ["es-CO"] = "Acceso denegado", ["en-US"] = "Access denied" },
            ["Login.NoAuthorized.Text"] = new()
                { ["es-CO"] = "No tienes permisos para acceder.", ["en-US"] = "You do not have permission to access." },
            ["App.Home.Text1"] = new() { ["es-CO"] = "Bienvenido a la aplicación", ["en-US"] = "Welcome to the app" },
            ["App.Home.Text2"] = new()
                { ["es-CO"] = "Seleccione una opción del menú", ["en-US"] = "Select an option from the menu" },
            ["General.Cultures"] = new() { ["es-CO"] = "Culturas", ["en-US"] = "Cultures" },
            ["General.Culture"] = new() { ["es-CO"] = "Cultura", ["en-US"] = "Culture" },
            ["General.Dashboard"] = new() { ["es-CO"] = "Panel", ["en-US"] = "Dashboard" },
            ["Menu.Home"] = new() { ["es-CO"] = "Inicio", ["en-US"] = "Home" },
            ["Menu.Authorization"] = new() { ["es-CO"] = "Autorización", ["en-US"] = "Authorization" },
            ["Menu.OrganizationalManagement"] = new() { ["es-CO"] = "Gestión Organizacional", ["en-US"] = "Organizational Management" },
            ["Menu.Administration"] = new() { ["es-CO"] = "Administración", ["en-US"] = "Administration" },
            ["Menu.Users"] = new() { ["es-CO"] = "Usuarios", ["en-US"] = "Users" },
            ["Menu.Companies"] = new() { ["es-CO"] = "Compañías", ["en-US"] = "Companies" },
            ["Menu.Actions"] = new() { ["es-CO"] = "Acciones", ["en-US"] = "Actions" },
            ["Menu.UsersRoles"] = new() { ["es-CO"] = "Roles de usuario", ["en-US"] = "User roles" },
            ["Menu.Features"] = new() { ["es-CO"] = "Funcionalidades", ["en-US"] = "Features" },
            ["Menu.Roles"] = new() { ["es-CO"] = "Roles", ["en-US"] = "Roles" },
            ["Menu.Permissions"] = new() { ["es-CO"] = "Permisos", ["en-US"] = "Permissions" },
            ["Menu.User"] = new() { ["es-CO"] = "Usuario", ["en-US"] = "User" },
            ["Menu.Resources"] = new() { ["es-CO"] = "Recursos", ["en-US"] = "Resources" },
            ["Menu.Cultures"] = new() { ["es-CO"] = "Culturas", ["en-US"] = "Cultures" },
            ["General.Actions"] = new() { ["es-CO"] = "Acciones", ["en-US"] = "Actions" },
            ["General.Action"] = new() { ["es-CO"] = "Acción", ["en-US"] = "Action" },
            ["General.Feature"] = new() { ["es-CO"] = "Funcionalidades", ["en-US"] = "Feature" },
            ["General.Search"] = new() { ["es-CO"] = "Buscar", ["en-US"] = "Search" },
            ["General.Company"] = new() { ["es-CO"] = "Compañía", ["en-US"] = "Company" },
            ["General.Nid"] = new() { ["es-CO"] = "Id", ["en-US"] = "Id" },
            ["General.FirstName"] = new() { ["es-CO"] = "Nombre", ["en-US"] = "First name" },
            ["General.LastName"] = new() { ["es-CO"] = "Apellido", ["en-US"] = "Last name" },
            ["General.Phone"] = new() { ["es-CO"] = "Teléfono", ["en-US"] = "Phone" },
            ["General.Nit"] = new() { ["es-CO"] = "Nit", ["en-US"] = "Nit" },
            ["General.Country"] = new() { ["es-CO"] = "Pais", ["en-US"] = "Country" },
            ["General.State"] = new() { ["es-CO"] = "Departamento", ["en-US"] = "State" },
            ["General.City"] = new() { ["es-CO"] = "Ciudad", ["en-US"] = "City" },
            ["General.Email"] = new() { ["es-CO"] = "Correo electrónico", ["en-US"] = "Email" },
            ["General.Code"] = new() { ["es-CO"] = "Código", ["en-US"] = "Code" },
            ["General.Name"] = new() { ["es-CO"] = "Nombre", ["en-US"] = "Name" },
            ["General.CreationDate"] = new() { ["es-CO"] = "Fecha de creación", ["en-US"] = "Creation date" },
            ["General.UpdateDate"] = new() { ["es-CO"] = "Fecha de actualización", ["en-US"] = "Update date" },
            ["General.Id"] = new() { ["es-CO"] = "Identificador", ["en-US"] = "Identifier" },
            ["General.Create"] = new() { ["es-CO"] = "Crear", ["en-US"] = "Create" },
            ["General.Cancel"] = new() { ["es-CO"] = "Cancelar", ["en-US"] = "Cancel" },
            ["General.Update"] = new() { ["es-CO"] = "Actualizar", ["en-US"] = "Update" },
            ["General.Delete"] = new() { ["es-CO"] = "Eliminar", ["en-US"] = "Delete" },
            ["General.Close"] = new() { ["es-CO"] = "Cerrar", ["en-US"] = "Close" },
            ["General.Search"] = new() { ["es-CO"] = "Buscar", ["en-US"] = "Search" },
            ["General.Role"] = new() { ["es-CO"] = "Rol", ["en-US"] = "Role" },
            ["Cultures.Title"] = new() { ["es-CO"] = "Gestión de Culturas", ["en-US"] = "Culture Management" },
            ["Resources.Title"] = new() { ["es-CO"] = "Gestión de Recursos", ["en-US"] = "Resource Management" },
            ["Actions.Title"] = new() { ["es-CO"] = "Gestión de Acciones", ["en-US"] = "Action Management" },
            ["Features.Title"] = new() { ["es-CO"] = "Gestión de Funcionalidades", ["en-US"] = "Feature Management" },
            ["Roles.Title"] = new() { ["es-CO"] = "Gestión de Roles", ["en-US"] = "Role Management" },
            ["Permissions.Title"] = new() { ["es-CO"] = "Gestión de Permisos", ["en-US"] = "Permission Management" },
            ["Companies.Title"] = new() { ["es-CO"] = "Gestión de Compañías", ["en-US"] = "Company Management" },
            ["Users.Title"] = new() { ["es-CO"] = "Gestión de Usuarios", ["en-US"] = "User Management" },
            ["UserRoles.Title"] = new()
                { ["es-CO"] = "Gestión de Roles de Usuario", ["en-US"] = "User Role Management" },
        };
    }
}