using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelTogether2.Models;

namespace TravelTogether2.Controllers
{
    [Route("api/v1.0/haslanguages")]
    [ApiController]
    public class HasLanguagesController : ControllerBase
    {
        private readonly TourGuide_v2Context _context;

        public HasLanguagesController(TourGuide_v2Context context)
        {
            _context = context;
        }

        // GET: api/HasLanguages
        /// <summary>
        /// Get list all HasLanguage
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HasLanguage>>> GetHasLanguages()
        {
            try
            {
                var result = await (from HasLanguage in _context.HasLanguages
                                    select new
                                    {
                                        HasLanguage.Id,
                                        HasLanguage.TourGuideId,
                                        HasLanguage.LanguageId
                                    }
                              ).ToListAsync();

                return Ok(new { StatusCodes = 200, message = "The request was successfully completed", data = result });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }

        }

        // GET: api/HasLanguages/5


        /// <summary>
        /// Get  HasLanguage by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<HasLanguage>> GetHasLanguage(int id)
        {
            try
            {
                var result = await (from HasLanguage in _context.HasLanguages
                                    where HasLanguage.Id == id
                                    select new
                                    {
                                        HasLanguage.Id,
                                        HasLanguage.TourGuideId,
                                        HasLanguage.LanguageId
                                    }
                              ).ToListAsync();

                return Ok(new { StatusCodes = 200, message = "The request was successfully completed", data = result });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }

        }

        // PUT: api/HasLanguages/5

        /// <summary>
        /// Edit HasLanguage by id
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHasLanguage(int id, HasLanguage hasLanguage)
        {
            try
            {
                var hasLan = _context.HasLanguages.Find(id);
                var TourgideId = _context.TourGuides.FirstOrDefault(x => x.Id == hasLanguage.TourGuideId);
                var LanguageId = _context.Languages.FirstOrDefault(x => x.Id == hasLanguage.LanguageId);

                hasLan.LanguageId = hasLanguage.LanguageId;
                hasLan.TourGuideId = hasLanguage.TourGuideId;
                if (!HasLanguageExists(hasLanguage.Id = id))
                {
                    return BadRequest(new { StatusCode = 404, Message = "ID Not Found!" });
                }

                if (TourgideId == null)
                {
                    return BadRequest(new { StatusCode = 404, Message = "Tourgide id is not found!" });
                }
                else if (LanguageId == null)
                {
                    return BadRequest(new { StatusCode = 404, Message = "LanguageId id is not found!" });
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

        // POST: api/HasLanguages


        /// <summary>
        /// Create HasLanguage by id
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<HasLanguage>> PostHasLanguage(HasLanguage hasLanguage)
        {
            try
            {
                var hasLan = new HasLanguage();
                var TourgideId = _context.TourGuides.FirstOrDefault(x => x.Id == hasLanguage.TourGuideId);
                var LanguageId = _context.Languages.FirstOrDefault(x => x.Id == hasLanguage.LanguageId);
                hasLan.LanguageId = hasLanguage.LanguageId;
                hasLan.TourGuideId = hasLanguage.TourGuideId;

                if (TourgideId == null)
                {
                    return BadRequest(new { StatusCode = 404, Message = "Tourgide id is not found!" });
                }
                else if (LanguageId == null)
                {
                    return BadRequest(new { StatusCode = 404, Message = "LanguageId id is not found!" });
                }
                else
                {
                    _context.HasLanguages.Add(hasLanguage);
                    await _context.SaveChangesAsync();
                    return Ok(new { status = 200, message = "Create hasLanguage successful!" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // DELETE: api/HasLanguages/5

        /// <summary>
        /// Delete HasLanguage by id (not use)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHasLanguage(int id)
        {
            var hasLanguage = await _context.HasLanguages.FindAsync(id);
            if (hasLanguage == null)
            {
                return NotFound();
            }

            _context.HasLanguages.Remove(hasLanguage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HasLanguageExists(int id)
        {
            return _context.HasLanguages.Any(e => e.Id == id);
        }
    }
}
