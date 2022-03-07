using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TravelTogether2.Helpers;
using TravelTogether2.Models;

namespace TravelTogether2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TourGuide_v2Context _context;
        private readonly AppSettings _appSettings;

        public UserController(TourGuide_v2Context context, IOptions<AppSettings> AppSettings)
        {
            _context = context;
            _appSettings = AppSettings.Value;
        }


        //=========================================================================test ==>test oke rồi
        [HttpPost("Login")]
        public async Task<ActionResult<IEnumerable<Account>>> Login(LoginVM login)
        {
            var acc = _context.Accounts.FirstOrDefault(acc => acc.Email == login.UserName && acc.Password == login.Password);
           
            if (acc == null)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "Your login attempt was not successful. Please try again."
                });
            }
            //generate token
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, acc.Email),
                new Claim(ClaimTypes.Role, "User")
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                success = true,
                message = "Login Successful!",
                data = new
                {
                    Token = tokenHandler.WriteToken(token)
                }
            });
        }


    }
}
