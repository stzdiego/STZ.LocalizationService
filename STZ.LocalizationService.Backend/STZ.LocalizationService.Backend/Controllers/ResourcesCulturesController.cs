using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STZ.LocalizationService.Access.DbContext;
using STZ.Shared.Bases;
using STZ.Shared.Entities;

namespace STZ.LocalizationService.Backend.Controllers;

public class ResourceCulturesController : StzControllerBase<ResourceCulture<Guid, Guid>>
{
    private readonly ILogger<ResourceCulturesController> _logger;
    private readonly ResourceServiceContext _context;

    public ResourceCulturesController(ILogger<ResourceCulturesController> logger, ResourceServiceContext context) : base(logger, context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("{cultureId:guid}", Name = "GetAllResourcesByCulture")]
    public async Task<IActionResult> GetAllResourcesByCultureAsync([FromRoute] Guid cultureId)
    {
        try
        {
            var resources = await _context.ResourceCultures
                .Where(r => r.CultureId.Equals(cultureId))
                .Include(r => r.Resource)
                .Select(r => new
                {
                    r.Resource!.Code,
                    r.Text
                })
                .ToListAsync();

            return Ok(resources);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(500, "Internal server error");
        }
    }
    
}