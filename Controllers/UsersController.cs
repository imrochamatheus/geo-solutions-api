using GeoSolucoesAPI.DTOs;
using GeoSolucoesAPI.Models;
using GeoSolucoesAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace GeoSolucoesAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtSettings _jwtSettings;

        public UsersController(IUserService userService, IOptions<JwtSettings> jwtSettings)
        {
            _userService = userService;
            _jwtSettings = jwtSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Authenticate(LoginDTO model)
        {
            var user = await _userService.Authenticate(model);
            if (user == null)
                return Unauthorized(new { message = "Email ou senha inválidos." });

            var jwt = GenerateJwtToken(user);
            return Ok(new { jwtToken = jwt });
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Create(UserDTO model)
        {
            try
            {
                var user = await _userService.CreateUser(model);
                return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateUserDTO model)
        {
            if (id != model.Id) return BadRequest();

            var success = await _userService.UpdateUser(id, model);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpPut("edit-user")]
        public async Task<ActionResult> UpdateProfile(UpdateUserDTO model)
        {
            var currentUserId = GetCurrentUserId();
            model.Id = currentUserId;

            try
            {
                var success = await _userService.UpdateUser(currentUserId, model);
                if (!success) return NotFound();

                var updatedUser = await _userService.GetById(currentUserId);
                var newJwt = GenerateJwtToken(updatedUser);

                return Ok(new { jwtToken = newJwt });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("change-password")]
        public async Task<ActionResult> ChangePassword(ChangePasswordDTO model)
        {
            var currentUserId = GetCurrentUserId();
            try
            {
                var success = await _userService.ChangePassword(currentUserId, model);
                if (!success) return BadRequest("Usuário não encontrado ou senha atual incorreta.");
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _userService.DeleteUser(id);
            if (!success) return NotFound();

            return NoContent();
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

            var claims = new ClaimsIdentity(new[]
            {
                new Claim("username", user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserType.ToString())
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpirationHours),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public class JwtSettings
        {
            public string SecretKey { get; set; }
            public int ExpirationHours { get; set; }
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out int userId))
                return userId;
            throw new UnauthorizedAccessException("Usuário não autorizado.");
        }
    }
}
