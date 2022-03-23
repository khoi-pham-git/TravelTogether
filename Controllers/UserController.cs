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
using System.Security.Cryptography;

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
            var acc = _context.Accounts.SingleOrDefault(acc => acc.Email == login.UserName && acc.Password == login.Password);

            if (acc == null)
            {
                return Ok(new ApiRespone
                {
                    Success = false,
                    Message = "Invalid username/password. Please try again."
                });
            }
            //generate token (cấp token)
            //var claims = new Claim[]
            //{
            //    new Claim(ClaimTypes.Name, acc.Email),
            //    new Claim(ClaimTypes.Role, "User")
            //};
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(claims),
            //    Expires = DateTime.UtcNow.AddDays(1),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};
            //var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new ApiRespone
            {
                Success = true,
                Message = "Authenticate Success!",
                //Data = new
                //{
                //    Token = tokenHandler.WriteToken(token)
                //}
                Data = GenerateToken(acc)
            });
        }


        private TokenModel GenerateToken(Account acc)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, acc.Email),
                    //role
                    new Claim("TokenId", Guid.NewGuid().ToString()),
                    new Claim("RoleId", acc.RoleId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var accessToken =  jwtTokenHandler.WriteToken(token);

            return new TokenModel
            {
                AccessToken = accessToken,
                RefeshToken = GenerateRefreshToken()
            };
        }

        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);

                return Convert.ToBase64String(random);
            }
        }
    }
}
