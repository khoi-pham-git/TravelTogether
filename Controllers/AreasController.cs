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
    [Route("api/v1.0/areas")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private readonly TourGuide_v2Context _context;

        public AreasController(TourGuide_v2Context context)
        {
            _context = context;
        }

        // GET: api/Areas
        // Get list area information - Luan


        [HttpGet("{ele}/{page}")]
        public async Task<ActionResult<IEnumerable<Area>>> GetAreas(int ele, int page)
        {
            try
            {
                var result = await (from area in _context.Areas
                                    select new
                                    {
                                        Id = area.Id,
                                        Name = area.Name,
                                        Description = area.Description,
                                        Latitude = area.Latitude,
                                        Longtitude = area.Longtitude
                                        //Khóa ngoại travel agenciesid
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
                return Ok(new { StatusCodes = 200, message = "The request was successfully completed", data = result });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }


        }

        // GET: api/Areas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Area>> GetArea(int id)
        {
            try
            {
                var result = await (from area in _context.Areas
                                    where area.Id == id
                                    select new
                                    {
                                        Id = area.Id,
                                        Name = area.Name,
                                        Description = area.Description,
                                        Latitude = area.Latitude,
                                        Longtitude = area.Longtitude
                                        //Khóa ngoại travel agenciesid
                                    }
                                ).ToListAsync();
                if (!result.Any())
                {
                    return BadRequest(new { StatusCode = 404, message = "ID is not found!" });
                }
                return Ok(new { StatusCodes = 200, message = "The request was successfully completed", data = result });

            }
            catch (Exception e)
            {

                return StatusCode(409, new { StatusCode = 409, message = e.Message });
            }
        }

        // PUT: api/Areas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArea(int id, Area area)
        {
            try
            {
                var area1 = _context.Areas.Find(id);
                var travelagencyid = new TravelAgency();
                if (!AreaExists(area.Id = id))
                {
                    return BadRequest(new { StatusCode = 404, Message = "ID Not Found!" });
                }

                if (!Validate.isName(area1.Name = area.Name))
                {
                    return BadRequest(new { StatusCode = 404, Message = "only character!" });
                }
                area1.Description = area.Description;
                area1.Latitude = area.Latitude;
                area1.Longtitude = area.Longtitude;
                //area1.TravelAgency=area.TravelAgency;
                var travelagencyid1 = _context.TravelAgencies.FirstOrDefault(s => s.Id == area.TravelAgencyId);
                if (travelagencyid1 == null)
                {
                    await _context.SaveChangesAsync();
                    return BadRequest(new { StatusCode = 404, Message = "ko có travel agency này!" });

                }


                return Ok(new { status = 200, message = "oke update rồi được chưa" });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, message = e.Message });
            }
        }

        // POST: api/Areas
      
        [HttpPost]
        
        public async Task<ActionResult<Area>> PostArea(Area area)
        {
            try
            {
                var area1 = new Area();
                var travelagencyid = new TravelAgency();

                if (!Validate.isName(area1.Name = area.Name))
                {
                    return BadRequest(new { StatusCode = 404, Message = "only character!" });
                }
                area1.Description = area.Description;
                area1.Latitude = area.Latitude;
                area1.Longtitude = area.Longtitude;
                area1.TravelAgencyId = area.TravelAgencyId;
                var travelagencyid1 = _context.TravelAgencies.FirstOrDefault(s => s.Id == area.TravelAgencyId);
                if (travelagencyid1 == null)
                {
                    return BadRequest(new { StatusCode = 404, Message = "ko có travel agency này!" });

                }

                _context.Areas.Add(area1);
                await _context.SaveChangesAsync();

                return Ok(new { status = 201, message = "Create area successfull!" });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, message = e.Message });
            }
        }

        // DELETE: api/Areas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArea(int id)
        {
            var area = await _context.Areas.FindAsync(id);
            if (area == null)
            {
                return NotFound();
            }

            _context.Areas.Remove(area);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AreaExists(int id)
        {
            return _context.Areas.Any(e => e.Id == id);
        }
    }
}
