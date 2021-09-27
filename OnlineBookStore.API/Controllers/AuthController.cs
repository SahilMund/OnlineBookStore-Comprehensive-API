using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineBookStore.API.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.API.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        OnlineBookStoreDBContext _dbcontext = new OnlineBookStoreDBContext();
        [HttpPost, Route("login")]
        public IActionResult LogIn([FromBody] LogInModel user)
        {
            if (user == null)
                return BadRequest("Invalid Client Request");
            var isUser = _dbcontext.Users.Count(s => s.Email == user.Email && s.Password == user.Password);
            var userData = _dbcontext.Users.Where(s => s.Email == user.Email && s.Password == user.Password);

            if (isUser > 0)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@032"));
                var signInCredential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOption = new JwtSecurityToken(
                    issuer: "https://localhost:44321",
                    audience: "https://localhost:44321",
                   
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signInCredential
                    );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOption);
                return Ok(new { Token=tokenString , userData=userData });
            }

            return Unauthorized("Invalid EmailId /Password");
        }
    }
}
