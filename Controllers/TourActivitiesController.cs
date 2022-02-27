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
    [Route("api/v1.0/touractivities")]

    [ApiController]
    public class TourActivitiesController : ControllerBase
    {
        private readonly TourGuide_v2Context _context;

        public TourActivitiesController(TourGuide_v2Context context)
        {
            _context = context;
        }

        // GET: api/TourActivities
        /// <summary>
        /// Get list all TourActivities
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TourActivity>>> GetTourActivities()
        {
            try
            {
                var result = await (from tourActivity in _context.TourActivities
                                    select new
                                    {
                                        tourActivity.Id,
                                        tourActivity.Name,
                                        tourActivity.Price,
                                        tourActivity.PlaceId,
                                        tourActivity.IsExtra,
                                        tourActivity.TourId,
                                        tourActivity.MainActivityId
                                    }).ToListAsync();
                return Ok(new { StatusCode = 200, message = "The request was successfully completed", data = result });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // GET: api/TourActivities/5
        /// <summary>
        /// Get TourActivities by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<TourActivity>> GetTourActivity(int id)
        {
            try
            {
                var result = await (from tourActivity in _context.TourActivities
                                    where tourActivity.Id == id
                                    select new
                                    {
                                        tourActivity.Id,
                                        tourActivity.Name,
                                        tourActivity.Price,
                                        tourActivity.PlaceId,
                                        tourActivity.IsExtra,
                                        tourActivity.TourId,
                                        tourActivity.MainActivityId
                                    }).ToListAsync();
                return Ok(new { StatusCode = 200, message = "The request was successfully completed", data = result });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // PUT: api/TourActivities/5
        /// <summary>
        /// Edit TourActivities by id
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTourActivity(int id, TourActivity tourActivity)
        {
            try
            {
                var tourActivity1 = _context.TourActivities.Find(id);
                var PlaceId = _context.Places.FirstOrDefault(x => x.Id == tourActivity.PlaceId);
                var TourId = _context.Tours.FirstOrDefault(x => x.Id == tourActivity.TourId);
                tourActivity1.Name = tourActivity.Name;
                tourActivity1.Price = tourActivity.Price;
                tourActivity1.PlaceId = tourActivity.PlaceId;
                tourActivity1.IsExtra = tourActivity.IsExtra;
                tourActivity1.TourId = tourActivity.TourId;
                tourActivity1.MainActivityId = tourActivity.MainActivityId;

                if (!TourActivityExists(tourActivity.Id = id))
                {
                    return BadRequest(new { StatusCodes = 404, Message = " Id not found!" });
                }
                if (PlaceId == null)
                {
                    return BadRequest(new { StatusCode = 404, Message = "Place id is not found!" });

                }
                else if (TourId == null)
                {
                    return BadRequest(new { StatusCode = 404, Message = "Tour id is not found!" });
                }
                else
                {
                    await _context.SaveChangesAsync();
                    return Ok(new { status = 200, message = "Update Tour Activity successful!" });

                }

            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // POST: api/TourActivities
        /// <summary>
        /// Create TourActivities
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<TourActivity>> PostTourActivity(TourActivity tourActivity)
        {
            try
            {
                var tourActivity1 = new TourActivity();
                var PlaceId = _context.Places.FirstOrDefault(x => x.Id == tourActivity.PlaceId);
                var TourId = _context.Tours.FirstOrDefault(x => x.Id == tourActivity.TourId);
                tourActivity1.Name = tourActivity.Name;
                tourActivity1.Price = tourActivity.Price;
                tourActivity1.PlaceId = tourActivity.PlaceId;
                tourActivity1.IsExtra = tourActivity.IsExtra;
                tourActivity1.TourId = tourActivity.TourId;
                tourActivity1.MainActivityId = tourActivity.MainActivityId;

                if (PlaceId == null)
                {
                    return BadRequest(new { StatusCode = 404, Message = "Place id is not found!" });

                }
                else if (TourId == null)
                {
                    return BadRequest(new { StatusCode = 404, Message = "Tour id is not found!" });
                }
                else
                {
                    _context.TourActivities.Add(tourActivity1);
                    await _context.SaveChangesAsync();
                    return Ok(new { status = 200, message = "Update Tour Activity successful!" });

                }

            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // DELETE: api/TourActivities/5
        /// <summary>
        /// Delete TourActivities by id (not use)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTourActivity(int id)
        {
            var tourActivity = await _context.TourActivities.FindAsync(id);
            if (tourActivity == null)
            {
                return NotFound();
            }

            _context.TourActivities.Remove(tourActivity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TourActivityExists(int id)
        {
            return _context.TourActivities.Any(e => e.Id == id);
        }
    }
}
