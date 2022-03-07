using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelTogether2.Common;
using TravelTogether2.Models;
using TravelTogether2.Services;

namespace TravelTogether2.Controllers
{
    [Route("api/v1.0/tours")]

    [ApiController]
    public class ToursController : ControllerBase
    {
        private readonly TourGuide_v2Context _context;
        private readonly ITourRespository  _tourRespository;

        public ToursController(TourGuide_v2Context context, ITourRespository tourRespository)
        {
            _context = context;
            _tourRespository = tourRespository;
        }

        // GET: api/Tours
        /// <summary>
        /// Get list all Tours
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tour>>> GetTours(string search, string sortby, int page = 1)
        {
            try
            {
                
                var result = _tourRespository.GetAll(search, sortby, page);
                var result1 = await (from c in _context.Tours select new
                {
                    c.Id
                }).ToListAsync(); 


                return Ok(new { StatusCodes = 200, message = "The request was successfully completed", data = result });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // GET: api/Tours/5
        //find tour by id
        /// <summary>
        /// Get Tours by id
        /// </summary>
        [HttpGet("id")]
        public async Task<ActionResult<Tour>> GetTour(int id)
        {
            try
            {
                var result = await (from tour in _context.Tours
                                    where tour.Id == id
                                    select new
                                    {
                                        tour.Id,
                                        tour.Name,
                                        tour.QuatityTrip,
                                        tour.Price,
                                        tour.Status,
                                        tour.TourGuideId
                                    }
                                   ).ToListAsync();


                return Ok(new { StatusCodes = 200, message = "The request was successfully completed", data = result });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }


        //// GET: api/Tours/5
        //// find tour by name
        ///// <summary>
        ///// Get Tours by name
        ///// </summary>
        //[HttpGet("name")]
        //public async Task<ActionResult<Tour>> GetTour(String name)
        //{
        //    try
        //    {
        //        var result = await (from tour in _context.Tours
        //                            where tour.Name.Contains(name)
        //                            select new
        //                            {
        //                                tour.Id,
        //                                tour.Name,
        //                                tour.QuatityTrip,
        //                                tour.Price,
        //                                tour.Status,
        //                                tour.TourGuideId
        //                            }
        //                           ).ToListAsync();


        //        return Ok(new { StatusCodes = 200, message = "The request was successfully completed", data = result });
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(409, new { StatusCode = 409, Message = e.Message });
        //    }
        //}


        // PUT: api/Tours/5

        //Edit tour - Luan
        /// <summary>
        /// Edit tour by id
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTour(int id, Tour tour)
        {
            try
            {
                var tour1 = _context.Tours.Find(id);
                var chTourguideId = _context.TourGuides.FirstOrDefault(x => x.Id == tour.TourGuideId);

                tour1.QuatityTrip = tour.QuatityTrip;
                tour1.Price = tour.Price;
                tour1.TourGuideId = tour.TourGuideId;
                if (!TourExists(tour.Id = id))
                {
                    return BadRequest(new { StatusCode = 404, Message = "ID Not Found!" });
                }
                if (chTourguideId == null)
                {
                    return BadRequest(new { StatusCode = 404, Message = "Tourgide id is not found!" });
                }
                else if (!Validate.isName(tour1.Name = tour.Name))
                {
                    return BadRequest(new { StatusCode = 404, Message = "Invalid Name!" });
                }
                else
                {
                    await _context.SaveChangesAsync();
                    return Ok(new { status = 200, message = "Update Tour successful!" });
                }

            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // PUT: api/Tours/5

        //Edit tour Status- Luan
        /// <summary>
        /// Edit Tours by id
        /// </summary>
        [HttpPut("status/{id}")]
        public async Task<IActionResult> PutTourStatus(int id)
        {
            try
            {
                var tour1 = _context.Tours.Find(id);
                if (!TourExists(id))
                {
                    return BadRequest(new { StatusCode = 404, Content = "Follow ID not found" });
                }
                if (tour1.Status == true)
                {
                    tour1.Status = false;
                }
                else
                {
                    tour1.Status = true;
                }
                await _context.SaveChangesAsync();
                return Ok(new { StatusCode = 200, Content = "The request has been completed successfully" }); // ok
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }




        // POST: api/Tours
        /// <summary>
        /// Create Tours
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Tour>> PostTour(Tour tour)
        {

            try
            {
                var tour1 = new Tour();
                var chTourguideId = _context.TourGuides.FirstOrDefault(x => x.Id == tour.TourGuideId);
                tour1.QuatityTrip = tour.QuatityTrip;
                tour1.Price = tour.Price;
                tour1.TourGuideId = tour.TourGuideId;
                tour1.Status = true;

                if (chTourguideId == null)
                {
                    return BadRequest(new { StatusCode = 404, Message = "Tourgide id is not found!" });
                }
                else if (!Validate.isName(tour1.Name = tour.Name))
                {
                    return BadRequest(new { StatusCode = 404, Message = "Invalid Name!" });
                }
                else
                {
                    _context.Tours.Add(tour1);
                    await _context.SaveChangesAsync();
                    return Ok(new { status = 200, message = "Create Tour successful!" });
                }

            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }
        // DELETE: api/Tours/5
        /// <summary>
        /// Delete Tours by id (not use)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTour(int id)
        {
            var tour = await _context.Tours.FindAsync(id);
            if (tour == null)
            {
                return NotFound();
            }

            _context.Tours.Remove(tour);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TourExists(int id)
        {
            return _context.Tours.Any(e => e.Id == id);
        }
    }
}
