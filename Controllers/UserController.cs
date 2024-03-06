using E_COMMERCE_WEBSITE.Context;
using E_COMMERCE_WEBSITE.Models;
using E_COMMERCE_WEBSITE.Models.DTO;
using E_COMMERCE_WEBSITE.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_COMMERCE_WEBSITE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase

    {
        private readonly IUser _user;
        private readonly IConfiguration _configuration;
        public static User user = new User();
        public UserController(IUser user, IConfiguration configuration)
        {
            _user = user;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] UserRegistrationDTO userdto)
        {
            
                var userexist = await _user.RegisterUser(userdto);
            
           if(!userexist) {
                return BadRequest("user already exist");
            }
            return Ok("user registered successfully");

            //string salt = BCrypt.Net.BCrypt.GenerateSalt();
            //string passwordHash = BCrypt.Net.BCrypt.HashPassword(userdto.passwordHash, salt);
            //userdto.passwordHash = passwordHash;
            //await _user.RegisterUser(userdto);
            //return Ok("user registered successfully");

        }

        [HttpPost("login")]

        public async Task<IActionResult> AuthenticateUser([FromBody] UserLoginDTO userlogndto)
        {
            var users = await _user.AuthenticateUser(userlogndto);

            //    if (users.email != userlogndto.email)
            //{
            //        return NotFound("User not found");
            //    }
            if (users == null)
            {
                return NotFound("User not found");

            }
            bool validatePassword = BCrypt.Net.BCrypt.Verify(userlogndto.password, users.passwordHash);
        if (!validatePassword) 
        {
                return BadRequest("Incorrect password");
            }

            string token = GenerateToken(users);

            return Ok(new { Token = token, email = users.email, password = users.passwordHash });

    }
        private string GenerateToken(User users)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, users.id.ToString()),
            new Claim(ClaimTypes.Name, users.username),
            new Claim(ClaimTypes.Role, users.Role),
            new Claim(ClaimTypes.Email, users.email),
        };

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddHours(1)

            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        [HttpGet("all-user")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllUser()
        {
            return Ok(await _user.GetAllUser());
        }
        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
      
       
        public async Task< IActionResult> GetUserById(int id)
        {
            return Ok( await _user.GetUserById(id));
        }
    }
}
