using CleanArchMVC.API.Models;
using CleanArchMVC.Domain.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMVC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticate _authentication;
        private readonly IConfiguration _configuration;

        public TokenController(IAuthenticate authentication, IConfiguration configuration)
        {
            _authentication = authentication ??
                throw new ArgumentNullException(nameof(authentication));

            _configuration = configuration;
        }

        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> LoginAsync([FromBody] LoginModel userInfo)
        {
            if (userInfo == null)
                return BadRequest("Invalid user info!");

            var result = await _authentication.AuthenticateAsync(userInfo.Email, userInfo.Password);

            if (result)
                return GenerateToken(userInfo);
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Unauthorized();
            }
        }

        [HttpPost("CreateUser")]
        //Used to hide API on Swagger
        //[ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult> CreateUserAsync([FromBody] LoginModel userInfo)
        {
            if (userInfo == null)
                return BadRequest("Invalid user info!");

            var result = await _authentication.RegisterUserAsync(userInfo.Email, userInfo.Password);

            if (result)
                return Ok($"User {userInfo.Email} was created successfully!");
            else
            {
                ModelState.AddModelError(string.Empty, "User couldn't be created.");
                return BadRequest();
            }
        }

        private UserToken GenerateToken(LoginModel userInfo)
        {
            var claims = new Claim[]
            {
                new Claim("email", userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(30);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
                );

            return new UserToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
