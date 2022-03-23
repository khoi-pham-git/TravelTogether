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
    [Route("api/v1.0/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly TourGuide_v2Context _context;

        public RolesController(TourGuide_v2Context context)
        {
            _context = context;
        }

        // GET: api/Roles
        /// <summary>
        /// Get list all Roles
        /// </summary                                                                                                                                //Luân
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            try
            {
                var  result = await(from role  in _context.Roles
                                    select new
                                    {
                                        role.Id, role.Name
                                    }).ToListAsync();

                return Ok(new { StatusCode = 200, message = "The request was successfully completed", data = result });

            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // GET: api/Roles/5
        /// <summary>
        /// Get Roles by id
        /// </summary>
        [HttpGet("{id}")]                                                                                                                                //Luân
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            try
            {
                var result = await (from role in _context.Roles
                                    where role.Id == id
                                    select new
                                    {
                                        role.Id,
                                        role.Name
                                    }).ToListAsync();

                return Ok(new { StatusCode = 200, message = "The request was successfully completed", data = result });

            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // PUT: api/Roles/5
        /// <summary>
        /// Edit Roles by id
        /// </summary>                                                                                                                                //Luân
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, Role role)
        {
            try
            {
                var role1 = _context.Roles.Find(id);
                if (!RoleExists(role.Id = id))
                {
                    return BadRequest(new { StatusCodes = 404, Message = " Id not found!" });
                }
                if (!Validate.isName(role1.Name = role.Name))
                {
                    return BadRequest(new { StatusCode = 400, Message = "Invalid Name" });
                }
                else
                {
                    await _context.SaveChangesAsync();
                    return Ok(new { status = 200, message = "Update Roles successful!" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // POST: api/Roles
        /// <summary>
        /// Create Roles                                                                                                                                //Luân
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(Role role)
        {
       
            try
            {
                var role1 = new Role();
                
                if (!Validate.isName(role1.Name = role.Name))
                {
                    return BadRequest(new { StatusCode = 400, Message = "Invalid Name" });
                }
                else
                {
                    _context.Roles.Add(role);
                    await _context.SaveChangesAsync();
                    return Ok(new { status = 200, message = "Update Roles successful!" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // DELETE: api/Roles/5
        /// <summary>                                                                                                                                //Luân
        /// Delete Roles by id (not use)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoleExists(int id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }
    }
}
