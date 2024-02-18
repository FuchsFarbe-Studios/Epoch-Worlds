// EpochWorlds
// MailController.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 18-2-2024
using EpochApp.Shared;
using EpochApp.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace EpochApp.Server.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;

        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        [HttpPost]
        public async Task<ActionResult> SendEmail(ContactDTO contactDTO)
        {
            await _mailService.SendEmail(contactDTO.Email, "Contact Form", contactDTO.Message);
            return Ok();
        }
    }
}