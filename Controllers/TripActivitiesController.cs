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
    [Route("api/v1.0/tripactivities")]                     // chuwa lamf

    [ApiController]
    public class TripActivitiesController : ControllerBase
    {
        private readonly TourGuide_v2Context _context;

        public TripActivitiesController(TourGuide_v2Context context)
        {
            _context = context;
        }

        // GET: api/TripActivities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TripActivity>>> GetTripActivities(int ele, int page)
        {
            try
            {
                var result = await (from tripActivity in _context.TripActivities
                                    select new
                                    {

                                        tripActivity.Id,
                                        tripActivity.StartTime,
                                        tripActivity.EndTime,
                                        tripActivity.Price,
                                        tripActivity.TripId,
                                        tripActivity.TourActivityId
                                    }
                                     ).ToListAsync();

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
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // GET: api/TripActivities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TripActivity>> GetTripActivity(int id)
        {
            try
            {
                var result = await (from tripActivity in _context.TripActivities
                                    where tripActivity.Id == id
                                    select new
                                    {

                                        tripActivity.Id,
                                        tripActivity.StartTime,
                                        tripActivity.EndTime,
                                        tripActivity.Price,
                                        tripActivity.TripId,
                                        tripActivity.TourActivityId
                                    }
                                     ).ToListAsync();


                return Ok(new { StatusCode = 200, message = "The request was successfully completed", data = result });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // PUT: api/TripActivities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTripActivity(int id, TripActivity tripActivity)
        {
            try
            {
                var tripActivity1 = _context.TripActivities.Find(id);
                var TripId = _context.Trips.FirstOrDefault(x => x.Id == tripActivity.TripId);
                var TourActivityId = _context.TourActivities.FirstOrDefault(x => x.Id == tripActivity.TourActivityId);
                tripActivity1.StartTime = tripActivity.StartTime;
                tripActivity1.EndTime = tripActivity.EndTime;
                tripActivity1.Price = tripActivity.Price;
                tripActivity1.TripId = tripActivity.TripId;
                tripActivity1.TourActivityId = tripActivity.TourActivityId;

                //check ton tai hay ko
                if (!TripActivityExists(tripActivity.Id = id))
                {
                    return BadRequest(new { StatusCode = 404, Message = "Id is not found!" });
                }

                if (TripId == null)
                {
                    return BadRequest(new { StatusCode = 404, Message = "Trip Id is not found!" });
                }
                else
                if (TourActivityId == null)
                {
                    return BadRequest(new { StatusCode = 404, Message = "TourActivity Id is not found!" });
                }
                else
                {
                    await _context.SaveChangesAsync();
                    return Ok(new { status = 200, message = "Update trip activity successful!" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // POST: api/TripActivities
        [HttpPost]
        public async Task<ActionResult<TripActivity>> PostTripActivity(TripActivity tripActivity)
        {
            try
            {
                var tripActivity1 = new TripActivity();
                var TripId = _context.Trips.FirstOrDefault(x => x.Id == tripActivity.TripId);
                var TourActivityId = _context.TourActivities.FirstOrDefault(x => x.Id == tripActivity.TourActivityId);
                tripActivity1.StartTime = tripActivity.StartTime;
                tripActivity1.EndTime = tripActivity.EndTime;
                tripActivity1.Price = tripActivity.Price;
                tripActivity1.TripId = tripActivity.TripId;
                tripActivity1.TourActivityId = tripActivity.TourActivityId;

                //check ton tai hay ko
                if (TripId == null)
                {
                    return BadRequest(new { StatusCode = 404, Message = "Trip Id is not found!" });
                }
                else
                if (TourActivityId == null)
                {
                    return BadRequest(new { StatusCode = 404, Message = "TourActivity Id is not found!" });
                }
                else
                {
                    _context.TripActivities.Add(tripActivity1);
                    await _context.SaveChangesAsync();
                    return Ok(new { status = 200, message = "Update trip activity successful!" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // DELETE: api/TripActivities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTripActivity(int id)
        {
            var tripActivity = await _context.TripActivities.FindAsync(id);
            if (tripActivity == null)
            {
                return NotFound();
            }

            _context.TripActivities.Remove(tripActivity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TripActivityExists(int id)
        {
            return _context.TripActivities.Any(e => e.Id == id);
        }
    }
}
