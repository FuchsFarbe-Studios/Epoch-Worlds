using EpochApp.Server.Data;
using EpochApp.Shared;
using EpochApp.Shared.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EpochApp.Server.Controllers
{
    /// <summary>
    ///     This controller is responsible for handling all user related requests for authorization/authentication.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EpochUsersController : ControllerBase
    {
        private const int keySize = 64;
        private const int iterations = 350000;
        private readonly EpochDataDbContext _context;
        private IConfiguration _configuration;
        private HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        /// <summary>
        ///     Constructor for the <see cref="EpochUsersController" />.
        /// </summary>
        /// <param name="context">
        ///     The injected <see cref="EpochDataDbContext" /> to use for the controller.
        /// </param>
        /// <param name="configuration">
        ///     The injected <see cref="IConfiguration" /> configuration settings.
        /// </param>
        public EpochUsersController(EpochDataDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        ///     GetUsersAsync() is a GET method which retrieves all the Users from Users DBSet
        /// </summary>
        /// <returns>
        ///     It returns a list of all User objects present in the users table.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        /// <summary>
        ///     GetUserAsync() is a GET method that retrieves a specific User based on the provided ID.
        /// </summary>
        /// <param name="id">
        ///     The GUID of the user to find.
        /// </param>
        /// <returns>
        ///     Returns a UserData object with user information if found, else Not Found error.
        /// </returns>
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

        /// <summary>
        ///     PutUserAsync() is a PUT method that updates a specific User based on the provided ID and User object.
        /// </summary>
        /// <param name="id">
        ///     The GUID of the User to update.
        /// </param>
        /// <param name="user">
        ///     User Object with new user details.
        /// </param>
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

        /// <summary>
        ///     PostUserAsync() is a POST method that creates a new User using a given User object.
        /// </summary>
        /// <param name="user">
        ///     User Entity that needs to be created in DB
        /// </param>
        /// <returns>
        ///     <see cref="Task{T}" /> where TResult is <see cref="ActionResult{T}" /> where TValue is <see cref="User" />.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<User>> PostUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserID }, user);
        }

        /// <summary>
        ///     AuthenticateAsync() is a POST method used for user login.
        /// </summary>
        /// <param name="login">
        ///     LoginDTO Entity that carries user login information
        /// </param>
        /// <returns>
        ///     Returns JWT if authentication success else error message.
        /// </returns>
        [HttpPost("Authentication")]
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
            var refreshToken = GenerateRefreshToken();
            await SetRefreshTokenAsync(user, refreshToken);
            return Ok(jwt);
        }

        [HttpPost("Refresh-Token")]
        public async Task<ActionResult<string>> GetRefreshToken()
        {
            var token = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(token))
                return BadRequest("No refresh token found");

            var user = await _context.Users
                                     .Include(user => user.UserRoles)
                                     .ThenInclude(userRole => userRole.Role)
                                     .FirstOrDefaultAsync(x => x.RefreshToken == token);
            if (user is null)
                return BadRequest("No user found with this refresh token");
            if (user.TokenExpires < DateTime.Now)
                return BadRequest("Refresh token has expired");

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
            var refreshToken = GenerateRefreshToken();
            await SetRefreshTokenAsync(user, refreshToken);
            return Ok(jwt);
        }

        [HttpPost("Verification")]
        public async Task<IActionResult> PostUserVerification(VerificationDTO verification)
        {
            var user = await _context.Users
                                     .Where(x => x.VerificationToken == verification.Token)
                                     .Include(user => user.UserRoles)
                                     .ThenInclude(userRole => userRole.Role)
                                     .FirstOrDefaultAsync();
            if (user is null)
                return BadRequest("Invalid token");
            if (user.VerificationTokenExpires < DateTime.Now)
                return BadRequest("Token has expired");

            user.IsVerified = true;
            user.VerificationToken = null;
            user.VerificationTokenCreated = null;
            user.VerificationTokenExpires = null;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

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
            var refreshToken = GenerateRefreshToken();
            await SetRefreshTokenAsync(user, refreshToken);
            return Ok(jwt);
        }

        private async Task SetRefreshTokenAsync(User user, RefreshToken refreshToken)
        {
            var cookieOpts = new CookieOptions
                             {
                                 Expires = refreshToken.TokenExpires,
                                 Secure = false,
                                 HttpOnly = true
                             };
            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOpts);
            user.RefreshToken = refreshToken.Token;
            user.TokenCreated = refreshToken.TokenCreated;
            user.TokenExpires = refreshToken.TokenExpires;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
                   {
                       Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                       TokenCreated = DateTime.Now,
                       TokenExpires = DateTime.Now.AddDays(7)
                   };
        }

        /// <summary>
        ///     RegisterAsync() is a POST method used for user registration.
        /// </summary>
        /// <param name="registration">
        ///     RegistrationDTO entity used carry new user registration information
        /// </param>
        /// <returns>
        ///     Returns user data if registration success else error message.
        /// </returns>
        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterAsync(RegistrationDTO registration)
        {
            // check if user already exists based on username or email
            var isUserAlreadyExists = await _context.Users.AnyAsync(u => u.UserName == registration.UserName || u.Email == registration.Email);
            if (isUserAlreadyExists)
            {
                ModelState.AddModelError(nameof(registration.UserName), "Username or Email already exists");
                ModelState.AddModelError(nameof(registration.Email), "Username or Email already exists");
                return BadRequest(ModelState);
            }

            // Other validations
            if (registration.DateOfBirth > DateTime.Now)
            {
                ModelState.AddModelError(nameof(registration.DateOfBirth), "Date of Birth cannot be in the future");
                return BadRequest(ModelState);
            }

            if (registration.UserName.Contains(" "))
            {
                ModelState.AddModelError(nameof(registration.UserName), "Username cannot contain spaces");
                return BadRequest(ModelState);
            }

            if (registration.Password.Length < 8)
            {
                ModelState.AddModelError(nameof(registration.Password), "Password must be at least 8 characters long");
                return BadRequest(ModelState);
            }

            if (registration.Password != registration.Password2)
            {
                ModelState.AddModelError(nameof(registration.Password2), "Passwords do not match!");
                return BadRequest(ModelState);
            }

            var user = new User
                       {
                           UserID = Guid.NewGuid(),
                           UserName = registration.UserName,
                           Email = registration.Email,
                           DateOfBirth = registration.DateOfBirth.Value
                       };

            var hash = HashPassword(registration.Password, out var salt);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            user.UserRoles.Add(new UserRole { DateAssigned = DateTime.Today, User = user, Role = await _context.Roles.FirstOrDefaultAsync(x => x.RoleID == 1) });
            user.Profile = new Profile();
            user.DateCreated = DateTime.Now;
            user.VerificationToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            user.VerificationTokenCreated = DateTime.Now;
            user.VerificationTokenExpires = DateTime.Now.AddDays(7);
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

        /// <summary>
        ///     Deletes a user from the database.
        /// </summary>
        /// <param name="id">
        ///     The id of the user to delete.
        /// </param>
        /// <returns>
        ///     <see cref="Task{T}" /> where TResult is <see cref="IActionResult" />.
        /// </returns>
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

        private string HashPassword(string password, out byte[] salt)
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

        private bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }

        private string CreateJWT(IEnumerable<Claim> claims)
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