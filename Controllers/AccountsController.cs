using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelTogether2.Common;
using TravelTogether2.Models;

namespace TravelTogether2.Controllers
{
    [Route("api/v1.0/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly TourGuide_v2Context _context;

        public AccountsController(TourGuide_v2Context context)
        {
            _context = context;
        }

        //Lấy list tào khoản account theo số lượng  và số trang là mấy

        // GET: api/Accounts
        [HttpGet("page/{ele}/{page}")]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts(int ele, int page)
        {

            var result = await (from account in _context.Accounts
                                select new
                                {
                                    Email = account.Email,
                                    Password = account.Password,
                                    RoleId = account.RoleId
                                }).ToListAsync();

            int totalEle = result.Count;
            int totalPage = Validate.totalPage(totalEle, ele);
            result = result.Skip((page - 1) * ele).Take(ele).ToList();
            if ((totalEle % ele) == 0)
            {
                totalPage = (totalEle / ele);
            }
            else
            {
                totalPage = (totalEle / ele) + 1;
            }

            return Ok(new { StatusCode = 200, message = "The request was successfully completed", data = result, totalEle, totalPage });
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(string id)
        {
            try
            {
                var result = await (from account in _context.Accounts
                                    where account.Email == id
                                    select new
                                    {
                                        Email = account.Email,
                                        Password = account.Password,
                                        RoleId = account.RoleId
                                    }).ToListAsync();

                if (!result.Any())
                {
                    return BadRequest(new { StatusCodes = 404, Message = " Account not found!" });
                }
                return Ok(new { StatusCode = 200, message = "The request was successfully completed", data = result });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }


        //Change Role
        // PUT: api/Accounts/5
     
        [HttpPut("Role/{email}")]
        public async Task<IActionResult> ChangeRole(string email, Account account)
        {
            try
            {   
                var account1 = _context.Accounts.Find(email);
                var rl1 = new Role();
                var rl = _context.Roles.FirstOrDefault(s => s.Id == account.RoleId);

                if (!AccountExists(account.Email = email))
                {
                    return BadRequest(new { StatusCode = 404, Message = "ID Not Found!" });
                }
                if (rl == null)
                {
                    return BadRequest(new { StatusCode = 404, Message = "Có role này đâu ?" });

                }
                await _context.SaveChangesAsync();

                return Ok(new { status = 200, message = "oke update rồi được chưa" });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        //Change password Luan
        [HttpPut("Password/{email}")]
        public async Task<IActionResult> ChangePassword(string email, Account account)
        {
            try
            {
                var account1 = _context.Accounts.Find(email);

                if (!AccountExists(account.Email = email))
                {
                    return BadRequest(new { StatusCode = 404, Message = "ID Not Found!" });
                }
                if (account1.Password == account.Password)
                {
                    return BadRequest(new { StatusCode = 400, Message = "Your new password is the same with current password!" });
                }
                account1.Password = account.Password.Trim();

                if (account.Password.Length < 8 || account.Password.Length > 16)
                {
                    return BadRequest(new { StatusCode = 400, Message = "Passwrod length must be 8 - 12" });
                }
                else if (!Validate.isLowerChar(account.Password))
                {
                    return BadRequest(new { StatusCode = 400, Message = "Password should contain At least one lower case letter" });
                }
                else if (!Validate.isUpperChar(account.Password))
                {
                    return BadRequest(new { StatusCode = 400, Message = "Password should contain At least one upper case letter" });
                }
                else if (!Validate.isNumber(account.Password))
                {
                    return BadRequest(new { StatusCode = 400, Message = "Password should contain At least one numeric value" });
                }
                else if (!Validate.isSymbols(account.Password))
                {
                    return BadRequest(new { StatusCode = 400, Message = "Password should contain At least one special case characters" });
                }
                else if (!Validate.isSpace(account.Password))
                {
                    return BadRequest(new { StatusCode = 400, Message = "Password should not space" });
                }
                else
                {
                    await _context.SaveChangesAsync();
                    return Ok(new { StatusCode = 200, Message = "Update Password Successfully!" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // POST: api/Accounts
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
            _context.Accounts.Add(account);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccountExists(account.Email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAccount", new { id = account.Email }, account);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountExists(string id)
        {
            return _context.Accounts.Any(e => e.Email == id);
        }
    }
}
