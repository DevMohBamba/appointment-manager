using AppointmentManager.Api.Data;
using AppointmentManager.Api.Models;
using AppointmentManager.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;


namespace AppointmentManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly AuthService _auth;


        public AuthController(ApplicationDbContext db, AuthService auth)
        {
            _db = db;
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto request)
        {
            var existingUser = await _db.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (existingUser != null)
                return BadRequest("Ce nom d'utilisateur est déjà pris.");

            var user = new User
            {
                
                Username = request.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            var token = _auth.CreateToken(user);
            return Ok(new { token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDto request)
        {
            Console.WriteLine($"Tentative de login pour {request.Username}");

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user == null)
            {
                Console.WriteLine("Utilisateur introuvable.");
                return Unauthorized("Nom d'utilisateur ou mot de passe invalide.");
            }

            if (string.IsNullOrEmpty(user.PasswordHash))
            {
                Console.WriteLine("Hash vide ou null.");
                return Unauthorized("Nom d'utilisateur ou mot de passe invalide.");
            }
            //BCrypt.NVerify(request.Password, user.PasswordHash);
            bool valid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!valid)
            {
                Console.WriteLine("Mot de passe invalide.");
                return Unauthorized("Nom d'utilisateur ou mot de passe invalide.");
            }

            Console.WriteLine("Connexion réussie.");
            var token = _auth.CreateToken(user);
            return Ok(new { token });
        }
    }

    public class UserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
