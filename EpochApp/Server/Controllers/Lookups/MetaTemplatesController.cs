using EpochApp.Server.Data;
using EpochApp.Shared;
using EpochApp.Shared.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MetaTemplatesController : ControllerBase
    {
        private readonly EpochDataDbContext _context;

        public MetaTemplatesController(EpochDataDbContext context)
        {
            _context = context;
        }

        // GET: api/MetaTemplates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MetaTemplateDTO>>> GetMetaTemplates()
        {
            return await _context.MetaTemplates
                                 .Include(t => t.Category)
                                 .Select(
                                     t => new MetaTemplateDTO
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
            var metaTemplate = await _context.MetaTemplates.Include(t=>t.Category).FirstOrDefaultAsync(t=>t.TemplateID == id);

            if (metaTemplate == null)
            {
                return NotFound();
            }

            return Ok(new MetaTemplateDTO
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
            return _context.MetaTemplates.Any(e => e.TemplateID == id);
        }
    }
}