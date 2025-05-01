using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using STZ.Shared.Bases;
using STZ.Shared.Entities;

namespace STZ.LocalizationService.Access.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class ResourceServiceContext : DbContextBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<ResourceServiceContext> _logger;
    private readonly IHttpContextAccessor? _httpContextAccessor;
    
    public DbSet<Resource> Resources { get; set; }
    public DbSet<Culture> Cultures { get; set; }
    public DbSet<ResourceCulture<Guid, Guid>> ResourceCultures { get; set; }
    
    public ResourceServiceContext(IConfiguration configuration, ILogger<ResourceServiceContext> logger, IHttpContextAccessor? httpContextAccessor = null) : base(configuration)
    {
        _configuration = configuration;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
}