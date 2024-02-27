// EpochWorlds
// SettingsController.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 2-2-2024
using EpochApp.Server.Data;
using EpochApp.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly EpochDataDbContext _context;

        public SettingsController(EpochDataDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientSettingDTO>>> GetSettings()
        {
            return await _context.ClientSettings.Select(c => new ClientSettingDTO
                                                             {
                                                                 SettingGroup = c.FieldName,
                                                                 SettingVal = c.SettingValue,
                                                                 FieldId = c.SettingFieldId
                                                             })
                                 .ToListAsync();
        }

        [HttpGet("{settingName}")]
        public async Task<ActionResult<IEnumerable<ClientSettingDTO>>> GetSettingsByName(string settingName)
        {
            var settings = await _context.ClientSettings.Where(x => x.FieldName.ToLower() == settingName.ToLower()).ToListAsync();
            if (!settings.Any())
                return NotFound();

            var settingData = settings.Select(c => new ClientSettingDTO
                                                   {
                                                       SettingGroup = c.FieldName,
                                                       SettingVal = c.SettingValue,
                                                       FieldId = c.SettingFieldId
                                                   });
            return Ok(settingData);
        }

        private bool SettingExists(int id)
        {
            return _context.ClientSettings.Any(e => e.SettingId == id);
        }
    }
}