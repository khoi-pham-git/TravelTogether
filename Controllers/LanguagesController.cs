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
    public class LanguagesController : ControllerBase
    {
        private readonly TourGuide_v2Context _context;

        public LanguagesController(TourGuide_v2Context context)
        {
            _context = context;
        }

        // GET: api/Languages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Language>>> GetLanguages()
        {
            try
            {
                var result = await (from Lan in _context.Languages
                                    select new
                                    {
                                    Lan.Id,
                                    Lan.Language1,
                                    Lan.Level
                                    }
                              ).ToListAsync();

                return Ok(new { StatusCodes = 200, message = "The request was successfully completed", data = result });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // GET: api/Languages/5
        [HttpGet("id")]
        public async Task<ActionResult<Language>> GetLanguage(int id)
        {

            try
            {
                var result = await (from Lan in _context.Languages
                                    where Lan.Id == id
                                    select new
                                    {
                                        Lan.Id,
                                        Lan.Language1,
                                        Lan.Level
                                    }
                              ).ToListAsync();

                return Ok(new { StatusCodes = 200, message = "The request was successfully completed", data = result });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // GET: api/Languages/5
        //Find by Name
        [HttpGet("name")]
        public async Task<ActionResult<Language>> GetLanguageByName(String name)
        {

            try
            {
                var result = await (from Lan in _context.Languages
                                    where Lan.Language1.Contains(name)  
                                    select new
                                    {
                                        Lan.Id,
                                        Lan.Language1,
                                        Lan.Level
                                    }
                              ).ToListAsync();

                return Ok(new { StatusCodes = 200, message = "The request was successfully completed", data = result });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }
        // PUT: api/Languages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLanguage(int id, Language language)
        {
            try
            {
                var language1 = _context.Languages.Find(id);

                if (!LanguageExists(language.Id = id))
                {
                    return BadRequest(new { StatusCode = 404, Message = "ID Not Found!" });
                }

                if (!Validate.isName(language1.Language1 = language.Language1))
                {
                    return BadRequest(new { StatusCode = 400, Message = "Only character!" });

                }
                else if (!Validate.isNumber(language1.Level = language.Level))
                {
                    return BadRequest(new { StatusCode = 400, Message = "Only number!" });

                }
                else
                {
                    await _context.SaveChangesAsync();
                    return Ok(new { status = 200, message = "Update Language successful!" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // POST: api/Languages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Language>> PostLanguage(Language language)
        {
            
            try
            {
                var language1 = new Language();

                if (!Validate.isName(language1.Language1 = language.Language1))
                {
                    return BadRequest(new { StatusCode = 400, Message = "Only character!" });

                }
                else if (!Validate.isNumber(language1.Level = language.Level))
                {
                    return BadRequest(new { StatusCode = 400, Message = "Only number!" });

                }
                else
                {
                    _context.Languages.Add(language);
                    await _context.SaveChangesAsync();
                    return Ok(new { status = 200, message = "Create Language successful!" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // DELETE: api/Languages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLanguage(int id)
        {
            var language = await _context.Languages.FindAsync(id);
            if (language == null)
            {
                return NotFound();
            }

            _context.Languages.Remove(language);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LanguageExists(int id)
        {
            return _context.Languages.Any(e => e.Id == id);
        }
    }
}
