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
    public class TravelAgenciesController : ControllerBase
    {
        private readonly TourGuide_v2Context _context;

        public TravelAgenciesController(TourGuide_v2Context context)
        {
            _context = context;
        }

        // GET: api/TravelAgencies
        //Get list TravelAgencies - Luan
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TravelAgency>>> GetTravelAgencies()
        {
            try
            {
                var result = await (from TravelAgency in _context.TravelAgencies
                                    select new
                                    {
                                        Id = TravelAgency.Id,
                                        Name = TravelAgency.Name,
                                        Address = TravelAgency.Address,
                                        Phone = TravelAgency.Phone,
                                        Description = TravelAgency.Description,
                                        Email = TravelAgency.Email,
                                        Image = TravelAgency.Image

                                    }
                                     ).ToListAsync();

                
                return Ok(new { StatusCode = 200, message = "The request was successfully completed", data = result, totalEle, totalPage });
            }
            catch (Exception e)
            {

                throw;
            }
        }

        // GET: api/TravelAgencies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TravelAgency>> GetTravelAgency(string id)
        {
            var travelAgency = await _context.TravelAgencies.FindAsync(id);

            if (travelAgency == null)
            {
                return NotFound();
            }

            return travelAgency;
        }

        // PUT: api/TravelAgencies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTravelAgency(string id, TravelAgency travelAgency)
        {
            if (id != travelAgency.Id)
            {
                return BadRequest();
            }

            _context.Entry(travelAgency).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TravelAgencyExists(id))
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

        // POST: api/TravelAgencies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TravelAgency>> PostTravelAgency(TravelAgency travelAgency)
        {
            _context.TravelAgencies.Add(travelAgency);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TravelAgencyExists(travelAgency.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTravelAgency", new { id = travelAgency.Id }, travelAgency);
        }

        // DELETE: api/TravelAgencies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTravelAgency(string id)
        {
            var travelAgency = await _context.TravelAgencies.FindAsync(id);
            if (travelAgency == null)
            {
                return NotFound();
            }

            _context.TravelAgencies.Remove(travelAgency);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TravelAgencyExists(string id)
        {
            return _context.TravelAgencies.Any(e => e.Id == id);
        }
    }
}
