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
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly TourGuide_v2Context _context;

        public PlacesController(TourGuide_v2Context context)
        {
            _context = context;
        }

        // GET: api/Places
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Place>>> GetPlaces(int ele, int page)
        {
            try
            {
                var  result = await (from place in _context.Places
                                     select new
                                     {
                                         place.Id,
                                         place.Name,
                                         place.Address,
                                         place.Description,
                                         place.Longtitude,
                                         place.Latitude,
                                         place.AreaId,
                                         place.CategoryId

                                     }
                                     ).ToListAsync();
                int totalEle = result.Count;
                int totalPage = Validate.totalPage(totalEle, ele);
                result = result.Skip((page - 1) * ele).Take(ele).ToList();

                return Ok(new { StatusCode = 200, message = "The request was successfully completed", data = result, totalEle, totalPage });



            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // GET: api/Places/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Place>> GetPlace(int id)
        {
            try
            {
                var result = await (from place in _context.Places
                                    where place.Id == id
                                    select new
                                    {
                                        place.Id,
                                        place.Name,
                                        place.Address,
                                        place.Description,
                                        place.Longtitude,
                                        place.Latitude,
                                        place.AreaId,
                                        place.CategoryId

                                    }
                                     ).ToListAsync();
               

                return Ok(new { StatusCode = 200, message = "The request was successfully completed", data = result});
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // PUT: api/Places/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlace(int id, Place place)
        {
            try
            {
                var place1 = _context.Places.Find(id);
                var areaId = _context.Areas.FirstOrDefault(s => s.Id == place.AreaId);
                var categoryId = _context.Categories.FirstOrDefault(s => s.Id == place.CategoryId);
                place1.Address = place.Address;
                place1.Description = place.Description;
                place1.Longtitude = place.Longtitude;
                place1.Latitude = place.Latitude;
                place1.AreaId = place.AreaId;
                place1.CategoryId = place.CategoryId;
                if (!PlaceExists(place.Id = id))
                {
                    return BadRequest(new { StatusCode = 404, Message = "ID Not Found!" });
                }
                if (areaId == null)
                {
                    return BadRequest(new { StatusCode = 404, Message = "Area id is not found!" });
                }
                else if (categoryId == null)
                {
                    return BadRequest(new { StatusCode = 404, Message = "Area id is not found!" });
                }
                else if (!Validate.isName(place1.Name = place.Name))
                {
                    return BadRequest(new { StatusCode = 400, Message = "invalid Name!" });
                }
                else
                {
                    await _context.SaveChangesAsync();
                    return Ok(new { status = 200, message = "Update Successful!" });

                }
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // POST: api/Places
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Place>> PostPlace(Place place)
        {
            try
            {
                var place1 = new Place();
                var areaId = _context.Areas.FirstOrDefault(s => s.Id == place.AreaId);
                var categoryId = _context.Categories.FirstOrDefault(s => s.Id == place.CategoryId);
                place1.Address = place.Address;
                place1.Description = place.Description;
                place1.Longtitude = place.Longtitude;
                place1.Latitude = place.Latitude;
                place1.AreaId = place.AreaId;
                place1.CategoryId = place.CategoryId;
                if (areaId == null)
                {
                    return BadRequest(new { StatusCode = 404, Message = "Area id is not found!" });
                }
                else if (categoryId == null)
                {
                    return BadRequest(new { StatusCode = 404, Message = "Area id is not found!" });
                }
                else if (!Validate.isName(place1.Name = place.Name))
                {
                    return BadRequest(new { StatusCode = 400, Message = "invalid Name!" });
                }
                else
                {
                    _context.Places.Add(place);
                    await _context.SaveChangesAsync();
                    return Ok(new { status = 200, message = "Update Successful!" });

                }
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // DELETE: api/Places/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlace(int id)
        {
            var place = await _context.Places.FindAsync(id);
            if (place == null)
            {
                return NotFound();
            }

            _context.Places.Remove(place);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlaceExists(int id)
        {
            return _context.Places.Any(e => e.Id == id);
        }
    }
}
