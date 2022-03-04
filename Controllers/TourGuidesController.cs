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
    [Route("api/v1.0/tourguide")]

    [ApiController]
    public class TourGuidesController : ControllerBase
    {
        private readonly TourGuide_v2Context _context;
        private readonly ITourGuidesRespository _tourGuidesRespository;

        public TourGuidesController(TourGuide_v2Context context, ITourGuidesRespository tourGuidesRespository)
        {
            _context = context;
            _tourGuidesRespository = tourGuidesRespository;
        }

        // GET: api/TourGuides
        /// <summary>
        /// Get list all TourGuides
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TourGuide>>> GetTourGuides(string search, string sortby, int page = 1)
        {
            try
            {
                var result = _tourGuidesRespository.GetAll(search, sortby, page);
                var result1 = await (from c in _context.TourGuides
                                     select new
                                     {
                                         c.Id
                                     }).ToListAsync();

                return Ok(new { StatusCode = 200, message = "The request was successfully completed", data = result});
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // GET: api/TourGuides/5
        /// <summary>
        /// Get TourGuides by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<TourGuide>> GetTourGuide(int id)
        {
            try
            {
                var result = await (from tourguidie in _context.TourGuides
                                    where tourguidie.Id == id
                                    select new
                                    {
                                        tourguidie.Id,
                                        tourguidie.Name,
                                        tourguidie.Dob,
                                        tourguidie.Gender,
                                        tourguidie.Phone,
                                        tourguidie.Email,
                                        tourguidie.SocialNumber,
                                        tourguidie.Certification,
                                        tourguidie.Address,
                                        tourguidie.Description,
                                        tourguidie.Rank,
                                        tourguidie.AreaId,          //check
                                        tourguidie.TravelAgencyId,  //check
                                        tourguidie.Image
                                    }
                                     ).ToListAsync();

                return Ok(new { StatusCode = 200, message = "The request was successfully completed", data = result });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // PUT: api/TourGuides/5 -Luan
        /// <summary>
        /// Edit TourGuides by id
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTourGuide(int id, TourGuide tourGuide)
        {
            try
            {
                var tourGuide1 = _context.TourGuides.Find(id);
                var AreaId = _context.Areas.FirstOrDefault(x => x.Id == tourGuide.AreaId);
                var TravelAgencyId = _context.TravelAgencies.FirstOrDefault(x => x.Id == tourGuide.TravelAgencyId);

                tourGuide1.Name = tourGuide.Name;
                tourGuide1.Dob = tourGuide.Dob;
                tourGuide1.Gender = tourGuide.Gender;
                tourGuide1.SocialNumber = tourGuide.SocialNumber;
                tourGuide1.Certification = tourGuide.Certification;
                tourGuide1.Address = tourGuide.Address;
                tourGuide1.Description = tourGuide.Description;
                tourGuide1.Rank = tourGuide.Rank;
                tourGuide1.AreaId = tourGuide.AreaId;
                tourGuide1.TravelAgencyId = tourGuide.TravelAgencyId;
                tourGuide1.Image = tourGuide.Image;


                //check id toonf taij
                if (!TourGuideExists(tourGuide.Id = id))
                {
                    return BadRequest(new { StatusCode = 404, Message = "Tourgide id is not found!" });
                }
                if (AreaId == null)
                {
                    return BadRequest(new { StatusCode = 404, Message = "AreaId is not found!" });
                }
                else if (TravelAgencyId == null)
                {
                    return BadRequest(new { StatusCode = 404, Message = "TravelAgencyId is not found!" });
                }
                else if (!Validate.isPhone(tourGuide1.Phone = tourGuide.Phone))
                {
                    return BadRequest(new { StatusCode = 400, Message = "Invalid phone number!" });
                }
                else if (!Validate.isEmail(tourGuide1.Email = tourGuide.Email))
                {
                    return BadRequest(new { StatusCode = 400, Message = "Invalid phone Email!" });
                }
                else
                {
                    await _context.SaveChangesAsync();
                    return Ok(new { status = 200, message = "Update TourGuide successful!" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }


        // POST: api/TourGuides
        /// <summary>
        /// Create TourGuides
        /// </summary>
        /// <remarks>
        /// Sample value of message
        /// 
        ///      POST /Todo
        ///     {
        ///         "name": "Toan1",
        ///         "gender": true,
        ///         "phone": "0961449382",
        ///         "email": "Toan@gmail.com",
        ///         "address": "HCM",
        ///         "rank": 1,
        ///         "image": "Toan.pmg"
        ///     }
        /// 
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<TourGuide>> PostTourGuide(TourGuide tourGuide)
        {
            try
            {
                var tourGuide1 = new TourGuide();
                var AreaId = _context.Areas.FirstOrDefault(x => x.Id == tourGuide.AreaId);
                var TravelAgencyId = _context.TravelAgencies.FirstOrDefault(x => x.Id == tourGuide.TravelAgencyId);

                tourGuide1.Name = tourGuide.Name;
                //tourGuide1.Dob = tourGuide.Dob;
                tourGuide1.Gender = tourGuide.Gender;
                //tourGuide1.SocialNumber = tourGuide.SocialNumber;
                //tourGuide1.Certification = tourGuide.Certification;
                tourGuide1.Address = tourGuide.Address;
                //tourGuide1.Description = tourGuide.Description;
                tourGuide1.Rank = tourGuide.Rank;
                //tourGuide1.AreaId = tourGuide.AreaId;
                //tourGuide1.TravelAgencyId = tourGuide.TravelAgencyId;
                tourGuide1.Image = tourGuide.Image;

                //check id toonf taij
                //if (AreaId == null)
                //{
                //    return BadRequest(new { StatusCode = 404, Message = "AreaId is not found!" });
                //}
                //else if (TravelAgencyId == null)
                //{
                //    return BadRequest(new { StatusCode = 404, Message = "TravelAgencyId is not found!" });
                //}
                /*else*/
                if (!Validate.isPhone(tourGuide1.Phone = tourGuide.Phone))
                {
                    return BadRequest(new { StatusCode = 400, Message = "Invalid phone number!" });
                }
                else if (!Validate.isEmail(tourGuide1.Email = tourGuide.Email))
                {
                    return BadRequest(new { StatusCode = 400, Message = "Invalid phone Email!" });
                }
                else
                {
                    _context.TourGuides.Add(tourGuide1);
                    await _context.SaveChangesAsync();
                    return Ok(new { status = 200, message = "Create TourGuide successful!" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // DELETE: api/TourGuides/5
        /// <summary>
        /// Delete TourGuides by id (not use)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTourGuide(int id)
        {
            var tourGuide = await _context.TourGuides.FindAsync(id);
            if (tourGuide == null)
            {
                return NotFound();
            }

            _context.TourGuides.Remove(tourGuide);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TourGuideExists(int id)
        {
            return _context.TourGuides.Any(e => e.Id == id);
        }
    }
}
