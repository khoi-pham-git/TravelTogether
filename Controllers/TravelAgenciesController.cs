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
            try
            {
                var travelagency1 = _context.TravelAgencies.Find(id);
                travelagency1.Address = travelAgency.Address;
                travelagency1.Description = travelAgency.Description;
                travelagency1.Image = travelAgency.Image;
                if (!TravelAgencyExists(travelAgency.Id = id))
                {
                    return BadRequest(new { StatusCode = 404, Message = "id is not found!" });
                }

                if (!Validate.isName(travelagency1.Name = travelAgency.Name))
                {
                    return BadRequest(new { StatusCode = 400, Message = "Invalid Name" });

                }
                else if (!Validate.isPhone(travelagency1.Phone = travelAgency.Phone))
                {
                    return BadRequest(new { StatusCode = 400, Message = "Invalid phone number!" });
                }
                else if (!Validate.isEmail(travelagency1.Email = travelAgency.Email))
                {
                    return BadRequest(new { StatusCode = 400, Message = "Invalid Email!" });
                }
                else
                {
                    await _context.SaveChangesAsync();
                    return Ok(new { status = 200, message = "Update TravelAgency successful!" });
                }


            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // POST: api/TravelAgencies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TravelAgency>> PostTravelAgency(TravelAgency travelAgency)
        {
            try
            {
                var travelagency1 = new TravelAgency();

                travelagency1.Id = travelAgency.Id;
                travelagency1.Address = travelAgency.Address;
                travelagency1.Description = travelAgency.Description;
                travelagency1.Image = travelAgency.Image;

                if (!Validate.isName(travelagency1.Name = travelAgency.Name))
                {
                    return BadRequest(new { StatusCode = 400, Message = "Invalid Name" });

                }
                else if (!Validate.isPhone(travelagency1.Phone = travelAgency.Phone))
                {
                    return BadRequest(new { StatusCode = 400, Message = "Invalid phone number!" });
                }
                else if (!Validate.isEmail(travelagency1.Email = travelAgency.Email))
                {
                    return BadRequest(new { StatusCode = 400, Message = "Invalid Email!" });
                }
                else
                {
                    _context.TravelAgencies.Add(travelagency1);
                    await _context.SaveChangesAsync();
                    return Ok(new { status = 200, message = "Create TravelAgency successful!" });
                }


            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
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
