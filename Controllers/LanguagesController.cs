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
    [Route("api/v1.0/languages")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly TourGuide_v2Context _context;
        private readonly ILanguageRespository _languageRespository ;

        public LanguagesController(TourGuide_v2Context context, ILanguageRespository languageRespository)
        {
            _context = context;
            _languageRespository = languageRespository;
        }

        // GET: api/Languages                                                                                                                                //Luân
        /// <summary>
        /// Get list all language with pagination
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Language>>> GetLanguages(string search, string sortby, int page = 1)
        {
            try
            {
                var result = _languageRespository.GetAll(search, sortby, page);
                var result1 = await (from c in _context.Languages
                                     select new
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

        // GET: api/Languages/5
        /// <summary>
        /// Get a language by id                                                                                                                                //Luân
        /// </summary>
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

        //// GET: api/Languages/5
        ////Find by Name
        ///// <summary>
        ///// Get a language by name
        ///// </summary>
        //[HttpGet("name")]                                                                                                                                //Luân
        //public async Task<ActionResult<Language>> GetLanguageByName(String name)
        //{

        //    try
        //    {
        //        var result = await (from Lan in _context.Languages
        //                            where Lan.Language1.Contains(name)  
        //                            select new
        //                            {
        //                                Lan.Id,
        //                                Lan.Language1,
        //                                Lan.Level
        //                            }
        //                      ).ToListAsync();

        //        return Ok(new { StatusCodes = 200, message = "The request was successfully completed", data = result });
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(409, new { StatusCode = 409, Message = e.Message });
        //    }
        //}
        // PUT: api/Languages/5
        /// <summary>
        /// Edit a language by id
        /// </summary>
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

        // POST: api/Languages                                                                                                                                //Luân
        /// <summary>
        /// Create a language
        /// </summary>

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
        /// <summary>
        /// Delete a language by id (not use)                                                                                                                                //Luân
        /// </summary>
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
