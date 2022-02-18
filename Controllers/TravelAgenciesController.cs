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
    [Route("api/v1.0/travelAgencies")]
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
        [HttpGet("{ele}/{page}")]
        public async Task<ActionResult<IEnumerable<TravelAgency>>> GetTravelAgencies(int ele, int page)
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

        // GET: api/TravelAgencies/5
        // Get Travel agency by id - Luan
        [HttpGet("{id}")]
        public async Task<ActionResult<TravelAgency>> GetTravelAgency(string id)
        {
            try
            {
                var result = await (from TravelAgency in _context.TravelAgencies
                                    where TravelAgency.Id == id
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

        // PUT: api/TravelAgencies/5
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
