using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelTogether2.Common;
using TravelTogether2.Models;
using TravelTogether2.Services;

namespace TravelTogether2.Controllers
{
    [Route("api/v1.0/places")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly TourGuide_v2Context _context;
        private readonly IPlaceResponsotory _placeResponsotory ;

        public PlacesController(TourGuide_v2Context context, IPlaceResponsotory placeResponsotory)
        {
            _context = context;
            _placeResponsotory = placeResponsotory;
        }

        // GET: api/Places                                                                                                                                //Luân
        /// <summary>
        /// Get list all places
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Place>>> GetPlaces(string search, string sortby, int page = 1)
        {
            try
            {
                var result = _placeResponsotory.GetAll(search, sortby, page);
                var result1 = await (from c in _context.Places
                                     select new
                                     {
                                         c.Id
                                     }).ToListAsync();

                return Ok(new { StatusCode = 200, message = "The request was successfully completed", data = result });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // GET: api/Places/5
        /// <summary>
        /// Get places by id                                                                                                                                //Luân
        /// </summary>
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


                return Ok(new { StatusCode = 200, message = "The request was successfully completed", data = result });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // PUT: api/Places/5
        /// <summary>                                                                                                                                //Luân
        /// Edit places by id
        /// </summary>
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
        /// <summary>                                                                                                                                //Luân
        /// Create places
        /// </summary>
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
        /// <summary>
        /// Delete places by id (not use)                                                                                                                                //Luân
        /// </summary>
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
