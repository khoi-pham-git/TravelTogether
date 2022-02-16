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
    public class AreasController : ControllerBase
    {
        private readonly TourGuide_v2Context _context;

        public AreasController(TourGuide_v2Context context)
        {
            _context = context;
        }

        // GET: api/Areas
        // Get list area information - Luan


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Area>>> GetAreas(int ele, int page)
        {
            var result = await (from area in _context.Areas
                                select new
                                {
                                    Id = area.Id,
                                    Name = area.Name,
        
                                    Latitude = area.Latitude,
                                    Longtitude = area.Longtitude
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

            return Ok(result);
        }

        // GET: api/Areas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Area>> GetArea(int id)
        {
            var area = await _context.Areas.FindAsync(id);

            if (area == null)
            {
                return NotFound();
            }

            return area;
        }

        // PUT: api/Areas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArea(int id, Area area)
        {
            if (id != area.Id)
            {
                return BadRequest();
            }

            _context.Entry(area).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AreaExists(id))
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

        // POST: api/Areas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Area>> PostArea(Area area)
        {
            _context.Areas.Add(area);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArea", new { id = area.Id }, area);
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
