// EpochWorlds
// ContactController.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 17-2-2024
using EpochApp.Server.Data;
using EpochApp.Shared;
using EpochApp.Shared.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Controllers
{
    /// <summary>
    ///     Controller to handle user contacts.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly EpochDataDbContext _context;

        /// <summary>
        ///     Constructor for the <see cref="ContactController" />
        /// </summary>
        /// <param name="context">
        ///     The injected <see cref="EpochDataDbContext" /> to use for the controller
        /// </param>
        public ContactController(EpochDataDbContext context)
        {
            _context = context;
        }

        /// <summary>
        ///     Get all contact points.
        /// </summary>
        /// <returns> All contact points. </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDTO>>> GetContactPoints()
        {
            return await _context.ContactPoints.Select(x => new ContactDTO
                                                            {
                                                                UserName = x.UserName,
                                                                Email = x.Email,
                                                                Phone = x.Phone,
                                                                ContactType = x.ContactType,
                                                                Message = x.Message,
                                                                CreatedOn = x.CreatedOn,
                                                                ResolvedOn = x.ResolvedOn
                                                            })
                                 .ToListAsync();
        }

        [HttpGet("Internal")]
        public async Task<ActionResult<IEnumerable<InternalContactDTO>>> GetInternalContactPoints()
        {
            return await _context.ContactPoints.Select(x => new InternalContactDTO
                                                            {
                                                                ContactPointId = x.ContactPointId,
                                                                UserName = x.UserName,
                                                                Email = x.Email,
                                                                Phone = x.Phone,
                                                                ContactType = x.ContactType,
                                                                Message = x.Message,
                                                                CreatedOn = x.CreatedOn,
                                                                ResolvedOn = x.ResolvedOn
                                                            })
                                 .ToListAsync();
        }

        /// <summary> Post a contact point. </summary>
        /// <param name="contactDTO">
        ///     The contact point to post.
        /// </param>
        /// <returns>
        ///     <see cref="ActionResult{T}" /> where TValue is <see cref="ContactDTO" />.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<ContactDTO>> PostContactPoint(ContactDTO contactDTO)
        {
            var contactPoint = new ContactPoint
                               {
                                   UserName = contactDTO.UserName,
                                   Email = contactDTO.Email,
                                   Phone = contactDTO.Phone,
                                   ContactType = contactDTO.ContactType,
                                   Message = contactDTO.Message,
                                   CreatedOn = DateTime.Now,
                                   ResolvedOn = null
                               };
            _context.ContactPoints.Add(contactPoint);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContactPoint", new { id = contactPoint.ContactPointId }, contactDTO);
        }

        /// <summary>
        ///     Get a contact point by its id.
        /// </summary>
        /// <param name="id">
        ///     The id of the contact point to get.
        /// </param>
        /// <returns>
        ///     <see cref="IActionResult" />
        /// </returns>
        [HttpGet("/Contact/{id:long}")]
        public IActionResult GetContactPoint(long id)
        {
            var contactPoint = _context.ContactPoints.Find(id);
            if (contactPoint == null)
                return NotFound();

            return Ok(new ContactDTO
                      {
                          UserName = contactPoint.UserName,
                          Email = contactPoint.Email,
                          Phone = contactPoint.Phone,
                          ContactType = contactPoint.ContactType,
                          Message = contactPoint.Message,
                          CreatedOn = contactPoint.CreatedOn,
                          ResolvedOn = contactPoint.ResolvedOn
                      });
        }
    }

}