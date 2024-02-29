using EpochApp.Server.Data;
using EpochApp.Server.Services;
using EpochApp.Shared;
using EpochApp.Shared.Users;
using EpochApp.Shared.Worlds;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

// ReSharper disable EntityFramework.NPlusOne.IncompleteDataUsage

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
        private readonly IConfiguration _configuration;
        private readonly EpochDataDbContext _context;
        private readonly ILogger<EpochUsersController> _logger;
        private readonly IMailService _mail;
        private readonly Random _random = new Random();
        private readonly IWorldService _worldService;
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
        /// <param name="mail"> The injected <see cref="IMailService" /> mail service. </param>
        /// <param name="worldService"> The injected <see cref="IWorldService" /> world service. </param>
        /// <param name="logger"> The injected <see cref="ILogger{TCategoryName}" /> logger. </param>
        public EpochUsersController(EpochDataDbContext context, IConfiguration configuration, IMailService mail, IWorldService worldService, ILogger<EpochUsersController> logger)
        {
            _context = context;
            _configuration = configuration;
            _mail = mail;
            _worldService = worldService;
            _logger = logger;
        }

        /// <summary>
        ///     GetUsersAsync() is a GET method which retrieves all the Users from Users DBSet
        /// </summary>
        /// <returns>
        ///     It returns a list of all User objects present in the users table.
        /// </returns>
        [HttpGet]
        [Authorize(Roles = "ADMIN,INTERNAL")]
        public async Task<ActionResult<IEnumerable<UserData>>> GetUsersAsync()
        {
            return await _context.Users.Select(x => new UserData
                                                    {
                                                        UserID = x.UserID,
                                                        UserName = x.UserName,
                                                        Email = x.Email,
                                                        DateOfBirth = x.DateOfBirth,
                                                        Roles = x.UserRoles.Select(ur => ur.Role.Description).ToList()
                                                    })
                                 .ToListAsync();
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
                           Roles = user?.UserRoles?.Select(ur => ur?.Role?.Description).ToList()
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
                return BadRequest();

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
        [Authorize(Roles = "ADMIN,INTERNAL")]
        public async Task<ActionResult<User>> PostUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserID }, user);
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
            }
            if (registration.UserName.IsNullOrEmpty())
                ModelState.AddModelError(nameof(registration.UserName), "Username cannot be blank!");
            if (registration.Password.IsNullOrEmpty())
                ModelState.AddModelError(nameof(registration.Password), "Password cannot be blank!");
            if (registration.Password2.IsNullOrEmpty())
                ModelState.AddModelError(nameof(registration.Password2), "Password verification required!");
            if (registration.Email.IsNullOrEmpty())
                ModelState.AddModelError(nameof(registration.Email), "Email is a required field!");
            if (registration.WorldName.IsNullOrEmpty())
                ModelState.AddModelError(nameof(registration.WorldName), "You must have a world to create an account!");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Other validations
            if (registration.DateOfBirth > DateTime.Now)
                ModelState.AddModelError(nameof(registration.DateOfBirth), "Date of Birth cannot be in the future");
            if (registration.UserName.Contains(" "))
                ModelState.AddModelError(nameof(registration.UserName), "Username cannot contain spaces");
            if (registration.Password.Length < 8)
                ModelState.AddModelError(nameof(registration.Password), "Password must be at least 8 characters long");
            if (registration.Password != registration.Password2)
                ModelState.AddModelError(nameof(registration.Password2), "Passwords do not match!");
            if (registration.TermAgreement == false)
                ModelState.AddModelError(nameof(registration.TermAgreement), "You must agree to the terms and conditions!");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new User
                       {
                           UserID = Guid.NewGuid(),
                           UserName = registration.UserName,
                           Email = registration.Email,
                           DateOfBirth = registration.DateOfBirth ?? DateTime.UtcNow,
                           OwnedWorlds = new List<World>()
                       };
            var newWorld = await _worldService.CreateRegistrationWorldAsync(registration, user);
            var hash = HashPassword(registration.Password, out var salt);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            user.UserRoles.Add(new UserRole { DateAssigned = DateTime.Today, User = user, Role = await _context.Roles.FirstOrDefaultAsync(x => x.RoleID == 1) });
            user.Profile = new Profile();
            user.DateCreated = DateTime.UtcNow;
            user.VerificationToken = _random.NextInt64(1, 999999999999).ToString("D12");
            user.VerificationTokenCreated = DateTime.UtcNow;
            user.VerificationTokenExpires = DateTime.UtcNow.AddDays(7);
            _logger.LogInformation($"Verification Token: {user.VerificationToken}");
            user.OwnedWorlds.Add(newWorld);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            await _mail.SendVerificationEmailAsync(user.Email, user.UserName, user.VerificationToken);

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
            var roles = user.UserRoles.Select(ur => ur.Role).ToList();
            var verifiedRole = await _context.Roles.FirstOrDefaultAsync(x => x.RoleID == 2);

            if (user.IsVerified && !roles.Contains(verifiedRole))
            {
                _logger.LogInformation("User is verified but does not have the verified role. Adding verified role...");
                user.UserRoles.Add(new UserRole
                                   {
                                       RoleID = 2,
                                       DateAssigned = DateTime.UtcNow
                                   });
                _context.Entry(user).State = EntityState.Modified;
                _context.Update(user);
                await _context.SaveChangesAsync();
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
        public async Task<ActionResult<string>> GetRefreshTokenAsync()
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

        /// <summary>
        ///     Verifies a user using a token.
        /// </summary>
        /// <param name="verification">
        ///     The verification token.
        /// </param>
        /// <returns>
        ///     The JWT if the verification is successful.
        /// </returns>
        [AllowAnonymous]
        [HttpPost("Verification")]
        public async Task<IActionResult> PostUserVerificationAsync([FromBody] VerificationDTO verification)
        {
            if (verification == null || verification.Token.IsNullOrEmpty())
                return BadRequest("Invalid token");

            var verifyUser = await _context.Users
                                           .Where(x => x.VerificationToken == verification.Token)
                                           .Include(user => user.UserRoles)
                                           .ThenInclude(userRole => userRole.Role)
                                           .FirstOrDefaultAsync();
            if (verifyUser == null)
                return BadRequest("Invalid token");
            if (verifyUser.VerificationTokenExpires < DateTime.Now)
                return BadRequest("Token has expired");

            var userId = verifyUser.UserID;
            var user = await _context.Users.Include(user => user.UserRoles)
                                     .ThenInclude(userRole => userRole.Role)
                                     .FirstOrDefaultAsync(x => x.UserID == userId);
            _logger.LogInformation($"Verifying User\n\tUser ID: {userId} \n\tUserName: {user.UserName}\n\tVerification Token: {verification.Token}");
            verifyUser.IsVerified = true;
            verifyUser.VerificationToken = null;
            verifyUser.VerificationTokenCreated = null;
            verifyUser.VerificationTokenExpires = null;
            var verifiedRole = await _context.Roles.FirstOrDefaultAsync(x => x.RoleID == 2);
            if (verifyUser.UserRoles.All(x => x.RoleID != 2))
                verifyUser.UserRoles.Add(new UserRole
                                         {
                                             Role = verifiedRole,
                                             DateAssigned = DateTime.UtcNow
                                         });
            foreach (var role in verifyUser.UserRoles)
                _logger.LogInformation($"User Role: {role?.Role?.Description}");
            var data = new UserData
                       {
                           UserID = verifyUser.UserID,
                           UserName = verifyUser.UserName,
                           Hash = verifyUser.PasswordHash,
                           Email = verifyUser.Email,
                           DateOfBirth = verifyUser.DateOfBirth,
                           Roles = new List<string>()
                       };
            data.Roles.AddRange(verifyUser.UserRoles.Select(ur => ur?.Role?.Description?.ToUpper()));
            var jwt = CreateJWT(data.ToClaimsPrincipal().Claims);
            var refreshToken = GenerateRefreshToken();
            await SetRefreshTokenAsync(verifyUser, refreshToken);

            _context.Entry(verifyUser).State = EntityState.Modified;

            try
            {
                _context.Update(verifyUser);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!UserExists(verifyUser.UserID))
                {
                    _logger.LogError(ex, "User does not exist! Verification failed!"
                                         + "\n"
                                         + "\tVerification Token: "
                                         + verification.Token);
                    return NotFound("User does not exist! Verification failed.");
                }
                throw;
            }
            return Ok(jwt);
        }

        [AllowAnonymous]
        [HttpPost("Forgot-Password")]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordDTO forgotPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == forgotPassword.User.ToLower() || x.UserName.ToLower() == forgotPassword.User.ToLower());
            if (user == null)
            {
                ModelState.AddModelError(nameof(forgotPassword.User), "User not found");
                return BadRequest(ModelState);
            }
            var token = _random.NextInt64(1, 999999999999).ToString("D12");
            user.ResetToken = token;
            user.ResetTokenCreated = DateTime.UtcNow;
            user.ResetTokenExpires = DateTime.UtcNow.AddMinutes(15);
            await _context.SaveChangesAsync();
            await _mail.SendResetPasswordEmailAsync(user.Email, user.UserName, token);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("Reset-Password")]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordDTO forgotPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.ResetToken == forgotPassword.ResetToken);
            if (user == null)
            {
                ModelState.AddModelError(nameof(forgotPassword.ResetToken), "Invalid token");
                return BadRequest(ModelState);
            }
            if (user.ResetTokenExpires < DateTime.UtcNow)
            {
                ModelState.AddModelError(nameof(forgotPassword.ResetToken), "Token has expired");
                return BadRequest(ModelState);
            }
            var hash = HashPassword(forgotPassword.Password, out var salt);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            user.DateModified = DateTime.UtcNow;
            user.ResetToken = null;
            user.ResetTokenCreated = null;
            user.ResetTokenExpires = null;
            _context.Entry(user).State = EntityState.Modified;
            try
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!UserExists(user.UserID))
                {
                    _logger.LogError(ex, "Password reset failed!"
                                         + "\n\tReset Token: "
                                         + forgotPassword.ResetToken);
                    ModelState.AddModelError(nameof(forgotPassword.ResetToken), "Password reset failed");
                    return BadRequest(ModelState);
                }
                throw;
            }

            return Ok();
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
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            _configuration.GetSection("Jwt:Issuer").Value,
            _configuration.GetSection("Jwt:Audience").Value,
            claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}