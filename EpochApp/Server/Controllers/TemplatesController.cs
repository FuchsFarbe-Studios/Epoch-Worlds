using EpochApp.Server.Data;
using EpochApp.Shared;
using EpochApp.Shared.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Controllers
{
    /// <summary>
    ///     A controller for the templating services. This controller is used to retrieve the templates for the various worlds,
    ///     articles, and features.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TemplatesController : ControllerBase
    {
        private readonly EpochDataDbContext _context;

        public TemplatesController(EpochDataDbContext context)
        {
            _context = context;
        }

        // GET: api/MetaTemplates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TemplateDTO>>> GetMetaTemplates()
        {
            return await _context.MetaTemplates
                                 .Include(t => t.Category)
                                 .Select(
                                 t => new TemplateDTO
                                      {
                                          TemplateName = t.TemplateName,
                                          Description = t.Description,
                                          Placeholder = t.Placeholder,
                                          HelpText = t.HelpText,
                                          Category = t.Category
                                      })
                                 .ToListAsync();
        }

        // GET: api/MetaTemplates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MetaTemplate>> GetMetaTemplate(int id)
        {
            var metaTemplate = await _context.MetaTemplates.Include(t => t.Category).FirstOrDefaultAsync(t => t.TemplateId == id);

            if (metaTemplate == null)
            {
                return NotFound();
            }

            return Ok(new TemplateDTO
                      {
                          TemplateName = metaTemplate.TemplateName,
                          Description = metaTemplate.Description,
                          Placeholder = metaTemplate.Placeholder,
                          HelpText = metaTemplate.HelpText,
                          Category = metaTemplate.Category
                      });
        }

        private bool MetaTemplateExists(int id)
        {
            return _context.MetaTemplates.Any(e => e.TemplateId == id);
        }
    }
}