using ISM_Redesing.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly string? _jwtSecret;
        private readonly string? _jwt_audience;
        private readonly string? _jwt_issuer;

        public AuthenticationController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _jwtSecret = configuration["JWT_SECRET"];
            _jwt_audience = configuration["JWT_AUDIENCE"];
            _jwt_issuer = configuration["JWT_ISSUER"];
            if (_jwtSecret == null || _jwtSecret.Length == 0)
            {
                throw new ArgumentNullException("JWT_SECRET cannot be read from configuration -> (ENV variables)");
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] SignInDTO signInDTO)
        {
            var user = await _userManager.FindByNameAsync(signInDTO.UsernameOrEmail)
                ?? await _userManager.FindByEmailAsync(signInDTO.UsernameOrEmail);
            if (user != null && await _userManager.CheckPasswordAsync(user, signInDTO.Password))
            {
                var roles = await _userManager.GetRolesAsync(user);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    // Add other claims as needed
                };
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSecret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(12),
                    Audience = _jwt_audience,
                    Issuer = _jwt_issuer,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok(new { Token = tokenHandler.WriteToken(token) });
            }

            return Unauthorized();
        }
    }
}