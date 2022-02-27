using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelTogether2.Models;

namespace TravelTogether2.Controllers
{
    [Route("api/v1.0/follows")]
    [ApiController]
    public class FollowsController : ControllerBase
    {
        private readonly TourGuide_v2Context _context;

        public FollowsController(TourGuide_v2Context context)
        {
            _context = context;
        }

        // GET: api/Follows luan

        /// <summary>
        /// Get list Follow
        /// </summary>
        //get all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Follow>>> GetFollows()
        {
            try
            {
                var result = await (from follow in _context.Follows
                                    select new
                                    {
                                        follow.Id,
                                        CustomerId = follow.CustomerId,
                                        TourGuideId = follow.TourGuideId,
                                        Status = follow.Status
                                    }).ToListAsync();

                return Ok(new { StatusCodes = 200, message = "The request was successfully completed", data = result });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // GET: api/Follows/5 Luan
        // find by ID

        /// <summary>
        /// Get follow by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Follow>> GetFollow(int id)
        {
            try
            {
                var result = await (from follow in _context.Follows
                                    where follow.Id == id
                                    select new
                                    {
                                        follow.Id,
                                        CustomerId = follow.CustomerId,
                                        TourGuideId = follow.TourGuideId,
                                        Status = follow.Status
                                    }).ToListAsync();

                return Ok(new { StatusCodes = 200, message = "The request was successfully completed", data = result });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // PUT: api/Follows/5
        // edit information not status Luan

        /// <summary>
        /// Edit follow by id
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFollow(int id, Follow follow)
        {
            try
            {

                var follow1 = _context.Follows.Find(id);
                var CustomerId = _context.Customers.FirstOrDefault(x => x.Id == follow.CustomerId);
                var TourgideId = _context.TourGuides.FirstOrDefault(x => x.Id == follow.TourGuideId);

                if (!FollowExists(follow.Id = id))
                {
                    return BadRequest(new { StatusCode = 404, Message = "ID Not Found!" });
                }

                if (CustomerId == null)
                {
                    return BadRequest(new { StatusCode = 404, Message = "Customer id is not found!" });
                }
                else if (TourgideId == null)
                {
                    return BadRequest(new { StatusCode = 404, Message = "Tourgide id is not found!" });
                }
                else
                {
                    await _context.SaveChangesAsync();
                    return Ok(new { status = 200, message = "Update Follow successful!" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }



        // PUT: api/Follows/5
        /// <summary>
        /// Edit status follow by id
        /// </summary>
        /// 
        [HttpPut("status/{id}")]
        public async Task<IActionResult> StatusFollow(int id)
        {
            try
            {
                var follow1 = _context.Follows.Find(id);
                if (!FollowExists(id))
                {
                    return BadRequest(new { StatusCode = 404, Content = "Follow ID not found" });
                }
                if (follow1.Status == true)
                {
                    follow1.Status = false;
                }
                else 
                {
                    follow1.Status = true;
                }
                await _context.SaveChangesAsync();
                return Ok(new { StatusCode = 200, Content = "The request has been completed successfully" }); // ok
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // POST: api/Follows -Luan
        /// <summary>
        /// Creata follow
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Follow>> PostFollow(Follow follow)
        {
            
            try
            {
                var CustomerId = _context.Customers.FirstOrDefault(x => x.Id == follow.CustomerId);
                var TourgideId = _context.TourGuides.FirstOrDefault(x => x.Id == follow.TourGuideId);
                follow.Status = true;
                if (CustomerId == null)
                {
                    return BadRequest(new { StatusCode = 404, Message = "Customer id is not found!" });
                }
                else if (TourgideId == null)
                {
                    return BadRequest(new { StatusCode = 404, Message = "Tourgide id is not found!" });
                }
                else
                {
                    _context.Follows.Add(follow);
                    await _context.SaveChangesAsync();
                    return Ok(new { status = 201, message = "Create Follow successful!" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // DELETE: api/Follows/5
        /// <summary>
        /// Delete follow by id (not use)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFollow(int id)
        {
            var follow = await _context.Follows.FindAsync(id);
            if (follow == null)
            {
                return NotFound();
            }

            _context.Follows.Remove(follow);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FollowExists(int id)
        {
            return _context.Follows.Any(e => e.Id == id);
        }
    }
}
