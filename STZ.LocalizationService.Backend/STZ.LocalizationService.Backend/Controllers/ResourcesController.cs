using STZ.LocalizationService.Access.DbContext;
using STZ.Shared.Bases;
using STZ.Shared.Entities;

namespace STZ.LocalizationService.Backend.Controllers;

public class ResourcesController : StzControllerBase<Resource>
{
    private readonly ILogger<ResourcesController> _logger;
    private readonly ResourceServiceContext _context;

    public ResourcesController(ILogger<ResourcesController> logger, ResourceServiceContext context) : base(logger, context)
    {
        _logger = logger;
        _context = context;
    }
}