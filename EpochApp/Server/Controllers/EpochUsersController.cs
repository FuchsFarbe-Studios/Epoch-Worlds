using EpochApp.Server.Data;
using EpochApp.Shared.DataTransfer;
using EpochApp.Shared.Site.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EpochApp.Server.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EpochUsersController : ControllerBase
    {
        private const Int32 keySize = 64;
        private const Int32 iterations = 350000;
        private readonly EpochDataDbContext _context;
        private IConfiguration _configuration;
        private HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        public EpochUsersController(EpochDataDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/EpochUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/EpochUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserData>> GetUserAsync(Guid id)
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
        public async Task<IActionResult> PutUserAsync(Guid id, User user)
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
                throw;
            }

            return NoContent();
        }

        // POST: api/EpochUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserID }, user);
        }

        [HttpPost("Auth/Authentication")]
        public async Task<IActionResult> AuthenticateAsync(LoginDTO login)
        {
            var user = await _context.Users
                                     .Include(x => x.UserRoles)
                                     .ThenInclude(x => x.Role)
                                     .Include(x => x.Profile)
                                     .FirstOrDefaultAsync(x => x.UserName == login.UserName);
            if (user is null)
            {
                ModelState.AddModelError(nameof(LoginDTO.UserName), "Username or Password is incorrect");
                ModelState.AddModelError(nameof(LoginDTO.Password), "Username or Password is incorrect");
                return BadRequest(ModelState);
            }

            if (!VerifyPassword(login.Password, user.PasswordHash, user.PasswordSalt))
            {
                ModelState.AddModelError(nameof(LoginDTO.UserName), "Unauthorized!");
                return BadRequest(ModelState);
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


        [HttpPost("Auth/Registration")]
        public async Task<IActionResult> Register(RegistrationDTO registration)
        {
            // check if user already exists based on username or email
            var isUserAlreadyExists = await _context.Users.AnyAsync(u => u.UserName == registration.UserName || u.Email == registration.Email);
            if (isUserAlreadyExists)
            {
                ModelState.AddModelError("UserName", "Username or Email already exists");
                ModelState.AddModelError("Email", "Username or Email already exists");
                return BadRequest(ModelState);
            }

            var user = new User
                       {
                           UserID = Guid.NewGuid(),
                           UserName = registration.UserName,
                           Email = registration.Email,
                           DateOfBirth = registration.DateOfBirth.Value
                       };

            var hash = HashPasword(registration.Password, out var salt);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            user.UserRoles.Add(new UserRole { DateAssigned = DateTime.Today, User = user, Role = await _context.Roles.FirstOrDefaultAsync(x => x.RoleID == 1) });
            user.Profile = new Profile();
            user.DateCreated = DateTime.Now;
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

        private Boolean UserExists(Guid id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }

        private String HashPasword(String password, out Byte[] salt)
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

        private Boolean VerifyPassword(String password, String hash, Byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }

        private String CreateJWT(IEnumerable<Claim> claims)
        {
            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
            var credentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            "https://localhost:5001",
            "https://localhost:5001",
            claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}