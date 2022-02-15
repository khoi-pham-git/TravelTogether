using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelTogether2.Models;

namespace TravelTogether2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HasLanguagesController : ControllerBase
    {
        private readonly TourGuide_v2Context _context;

        public HasLanguagesController(TourGuide_v2Context context)
        {
            _context = context;
        }

        // GET: api/HasLanguages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HasLanguage>>> GetHasLanguages()
        {
            return await _context.HasLanguages.ToListAsync();
        }

        // GET: api/HasLanguages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HasLanguage>> GetHasLanguage(int id)
        {
            var hasLanguage = await _context.HasLanguages.FindAsync(id);

            if (hasLanguage == null)
            {
                return NotFound();
            }

            return hasLanguage;
        }

        // PUT: api/HasLanguages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHasLanguage(int id, HasLanguage hasLanguage)
        {
            if (id != hasLanguage.Id)
            {
                return BadRequest();
            }

            _context.Entry(hasLanguage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HasLanguageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/HasLanguages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HasLanguage>> PostHasLanguage(HasLanguage hasLanguage)
        {
            _context.HasLanguages.Add(hasLanguage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHasLanguage", new { id = hasLanguage.Id }, hasLanguage);
        }

        // DELETE: api/HasLanguages/5
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
