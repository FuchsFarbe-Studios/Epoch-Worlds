using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EpochApp.Data;
using EpochApp.Shared;
using Microsoft.IdentityModel.Tokens;

namespace EpochApp.Server
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EpochUsersController : ControllerBase
    {
        private readonly EpochDataDbContext _context;
        private IConfiguration _configuration;
        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        public EpochUsersController(EpochDataDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/EpochUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/EpochUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserData>> GetUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var data = new UserData
                       {
                           UserID = user.UserID,
                           UserName = user.UserName,
                           Hash = user.PasswordHash,
                           Email = user.Email,
                           DateOfBirth = user.DateOfBirth,
                           Roles = user.UserRoles.Select(ur => ur.Role.Description).ToList()
                       };

            return data;
        }

        // PUT: api/EpochUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (id != user.UserID)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EpochUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserID }, user);
        }

        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate(LoginDTO authentication)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == authentication.UserName);
            if (user is null)
            {
                return NotFound();
            }

            if (!VerifyPassword(authentication.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized();
            }

            var data = new UserData
                       {
                           UserID = user.UserID,
                           UserName = user.UserName,
                           Hash = user.PasswordHash,
                           Email = user.Email,
                           DateOfBirth = user.DateOfBirth,
                           Roles = user.UserRoles.Select(ur => ur.Role.Description.ToUpper()).ToList()
                       };
            var jwt = CreateJWT(data.ToClaimsPrincipal().Claims);
            return Ok(jwt);
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> Register(RegistrationDTO registration)
        {
            var user = new User { UserID = Guid.NewGuid(), UserName = registration.UserName, Email = registration.Email, DateOfBirth = registration.DateOfBirth };
            var hash = HashPasword(registration.Password, out var salt);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            user.UserRoles.Add(new UserRole { DateAssigned = DateTime.Today, User = user, Role = await _context.Roles.FirstOrDefaultAsync(x => x.RoleID == 1) });
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var data = new UserData
                       {
                           UserID = user.UserID,
                           UserName = user.UserName,
                           Hash = user.PasswordHash,
                           Email = user.Email,
                           DateOfBirth = user.DateOfBirth,
                           Roles = user.UserRoles.Select(ur => ur.Role.Description).ToList()
                       };

            return CreatedAtAction("GetUser", new { id = user.UserID }, data);
        }

        // DELETE: api/EpochUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }

        string HashPasword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);
            return Convert.ToHexString(hash);
        }

        bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }

        private string CreateJWT(IEnumerable<Claim> claims)
        {
            var secretkey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value.ToString()));
            var credentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "https://localhost:5001",
                audience: "https://localhost:5001",
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}