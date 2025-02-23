using GestionTache.Models;
using GestionTache.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GestionTache.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            bool result = await _userService.RegisterAsync(user.Username, user.PasswordHash);
            if (!result)
                return BadRequest("L'utilisateur existe déjà.");
            return Ok("Inscription réussie.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var authenticatedUser = await _userService.AuthenticateAsync(user.Username, user.PasswordHash);
            if (authenticatedUser == null)
                return Unauthorized("Nom d'utilisateur ou mot de passe incorrect.");
            return Ok(new { authenticatedUser.Username, authenticatedUser.Role });
        }
    }
}
