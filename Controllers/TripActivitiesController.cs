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
    [Route("api/[controller]")]
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
        public async Task<ActionResult<IEnumerable<TripActivity>>> GetTripActivities()
        {
            return await _context.TripActivities.ToListAsync();
        }

        // GET: api/TripActivities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TripActivity>> GetTripActivity(int id)
        {
            var tripActivity = await _context.TripActivities.FindAsync(id);

            if (tripActivity == null)
            {
                return NotFound();
            }

            return tripActivity;
        }

        // PUT: api/TripActivities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTripActivity(int id, TripActivity tripActivity)
        {
            if (id != tripActivity.Id)
            {
                return BadRequest();
            }

            _context.Entry(tripActivity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TripActivityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TripActivities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TripActivity>> PostTripActivity(TripActivity tripActivity)
        {
            _context.TripActivities.Add(tripActivity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTripActivity", new { id = tripActivity.Id }, tripActivity);
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
