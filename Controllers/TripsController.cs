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
    public class TripsController : ControllerBase
    {
        private readonly TourGuide_v2Context _context;

        public TripsController(TourGuide_v2Context context)
        {
            _context = context;
        }

        // GET: api/Trips
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trip>>> GetTrips(int ele, int page)
        {
            try
            {
                var result = await (from trip in _context.Trips
                                    select new
                                    {
                                        trip.Id,
                                        trip.BookingDate,
                                        trip.StartDate,
                                        trip.EndDate,
                                        trip.FeedBackCore,
                                        trip.Status,
                                        trip.Feedback,
                                        trip.Price,
                                        trip.TourId,
                                        trip.CustomerId
                                    }
                              ).ToListAsync();
                int totalEle = result.Count;
                int totalPage = Validate.totalPage(totalEle, ele);
                result = result.Skip((page - 1) * ele).Take(ele).ToList();
                return Ok(new { StatusCodes = 200, message = "The request was successfully completed", data = result, totalEle, totalPage });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // GET: api/Trips/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trip>> GetTrip(int id)
        {
            try
            {
                var result = await (from trip in _context.Trips
                                    where trip.Id == id
                                    select new
                                    {
                                        trip.Id,
                                        trip.BookingDate,
                                        trip.StartDate,
                                        trip.EndDate,
                                        trip.FeedBackCore,
                                        trip.Status,
                                        trip.Feedback,
                                        trip.Price,
                                        trip.TourId,
                                        trip.CustomerId
                                    }
                              ).ToListAsync();

                return Ok(new { StatusCodes = 200, message = "The request was successfully completed", data = result });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // PUT: api/Trips/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrip(int id, Trip trip)
        {
            try
            {
                var trip1 = _context.Trips.Find(id);
                var chTourId = _context.Tours.FirstOrDefault(x => x.Id == trip.TourId);
                var chCustomerId = _context.Customers.FirstOrDefault(x => x.Id == trip.CustomerId);
                trip1.TourId = trip.TourId;
                trip1.CustomerId = trip.CustomerId;
                trip1.BookingDate = trip.BookingDate;
                trip1.StartDate = trip.StartDate;
                trip1.EndDate = trip.EndDate;
                trip1.FeedBackCore = trip.FeedBackCore;
                trip1.Feedback = trip.Feedback;
                trip1.Price = trip.Price;
                if (!TripExists(trip.Id = id))
                {
                    return BadRequest(new { StatusCode = 404, Message = "ID Not Found!" });
                }
                if (chTourId == null)
                {
                    return BadRequest(new { StatusCode = 404, Message = "Tour id is not found!" });
                }
                else if (chCustomerId == null)
                {
                    return BadRequest(new { StatusCode = 404, Message = "Customer id is not found!" });
                }
                else
                {
                    await _context.SaveChangesAsync();
                    return Ok(new { status = 200, message = "Update hasLanguage successful!" });
                }

            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }




        // PUT: api/Trips/5
        [HttpPut("status/{id}")]
        public async Task<IActionResult> PutTrip(int id)
        {
            try
            {
                var trip = _context.Trips.Find(id);
                if (!TripExists(id))
                {
                    return BadRequest(new { StatusCode = 404, Content = "Follow ID not found" });
                }
                if (trip.Status == true)
                {
                    trip.Status = false;
                }
                else
                {
                    trip.Status = true;
                }
                await _context.SaveChangesAsync();
                return Ok(new { StatusCode = 200, Content = "The request has been completed successfully" }); // ok
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // POST: api/Trips
        [HttpPost]
        public async Task<ActionResult<Trip>> PostTrip(Trip trip)
        {
           
            try
            {
                var trip1 = new Trip();
                var chTourId = _context.Tours.FirstOrDefault(x => x.Id == trip.TourId);
                var chCustomerId = _context.Customers.FirstOrDefault(x => x.Id == trip.CustomerId);
                trip1.TourId = trip.TourId;
                trip1.CustomerId = trip.CustomerId;
                trip1.BookingDate = trip.BookingDate;
                trip1.StartDate = trip.StartDate;
                trip1.EndDate = trip.EndDate;
                trip1.FeedBackCore = trip.FeedBackCore;
                trip1.Feedback = trip.Feedback;
                trip1.Price = trip.Price;
                trip1.Status = true;
                if (chTourId == null)
                {
                    return BadRequest(new { StatusCode = 404, Message = "Tour id is not found!" });
                }
                else if (chCustomerId == null)
                {
                    return BadRequest(new { StatusCode = 404, Message = "Customer id is not found!" });
                }
                else
                {
                    _context.Trips.Add(trip1);
                    await _context.SaveChangesAsync();
                    return Ok(new { status = 200, message = "Update hasLanguage successful!" });
                }

            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // DELETE: api/Trips/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip(int id)
        {
            var trip = await _context.Trips.FindAsync(id);
            if (trip == null)
            {
                return NotFound();
            }

            _context.Trips.Remove(trip);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TripExists(int id)
        {
            return _context.Trips.Any(e => e.Id == id);
        }
    }
}
