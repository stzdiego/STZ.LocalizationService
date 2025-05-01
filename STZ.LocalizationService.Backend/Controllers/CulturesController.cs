using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STZ.LocalizationService.Access.DbContext;
using STZ.Shared.Bases;
using STZ.Shared.Dtos;
using STZ.Shared.Entities;

namespace STZ.LocalizationService.Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class CulturesController : StzControllerBase<Culture>
{
    private readonly ILogger<CulturesController> _logger;
    private readonly ResourceServiceContext _context;

    public CulturesController(ILogger<CulturesController> logger, ResourceServiceContext context) : base(logger, context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("{cultureId}/resources")]
    public async Task<IActionResult> GetResourcesAsync([FromRoute] string cultureId)
    {
        try
        {
            var cultureName = _context.Cultures.First(x => x.Id == Guid.Parse(cultureId)).Name;
            var resources = await _context.ResourceCultures
                .Where(x => x.CultureId == Guid.Parse(cultureId))
                .Include(x => x.Resource)
                .Select(x => new ResourceDto
                    {
                        Code = x.Resource!.Code,
                        Text = x.Text
                    })
                .ToListAsync();

            if (!resources.Any())
            {
                _logger.LogWarning("No se encontraron recursos para la cultura {CultureId}", cultureId);
                return NotFound($"No se encontraron recursos para la cultura {cultureId}");
            }
            
            return Ok(new ResourcesCultureDto
            {
                CultureCode = cultureName,
                Resources = resources
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al obtener los recursos de la cultura {CultureId}", cultureId);
            return StatusCode(500, "Error interno del servidor");
        }
    }
}