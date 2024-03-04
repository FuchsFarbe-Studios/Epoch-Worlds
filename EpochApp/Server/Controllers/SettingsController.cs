// EpochWorlds
// SettingsController.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 2-2-2024
using EpochApp.Server.Data;
using EpochApp.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

#pragma warning disable 1591// Missing XML comment for publicly visible type or member
// ReSharper disable UnusedMember.Local

namespace EpochApp.Server.Controllers
{
    /// <summary>
    ///    Settings for the client.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class SettingsController : ControllerBase
    {
        private readonly EpochDataDbContext _context;

        /// <summary>
        ///    Constructor for the settings controller.
        /// </summary>
        /// <param name="context"> The database context. </param>
        public SettingsController(EpochDataDbContext context)
        {
            _context = context;
        }

        /// <summary>
        ///   Get all client settings.
        /// </summary>
        /// <returns> A list of client settings. </returns>
        [HttpGet("ClientSettings")]
        public async Task<ActionResult<List<ClientSetting>>> IndexSettingsAsync()
        {
            return await _context.ClientSettings.ToListAsync();
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