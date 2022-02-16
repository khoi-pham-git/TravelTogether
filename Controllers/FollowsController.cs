﻿using System;
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
    public class FollowsController : ControllerBase
    {
        private readonly TourGuide_v2Context _context;

        public FollowsController(TourGuide_v2Context context)
        {
            _context = context;
        }

        // GET: api/Follows
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Follow>>> GetFollows()
        {
            return await _context.Follows.ToListAsync();
        }

        // GET: api/Follows/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Follow>> GetFollow(int id)
        {
            var follow = await _context.Follows.FindAsync(id);

            if (follow == null)
            {
                return NotFound();
            }

            return follow;
        }

        // PUT: api/Follows/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFollow(int id, Follow follow)
        {
            if (id != follow.Id)
            {
                return BadRequest();
            }

            _context.Entry(follow).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FollowExists(id))
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

        // POST: api/Follows
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Follow>> PostFollow(Follow follow)
        {
            _context.Follows.Add(follow);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFollow", new { id = follow.Id }, follow);
        }

        // DELETE: api/Follows/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFollow(int id)
        {
            var follow = await _context.Follows.FindAsync(id);
            if (follow == null)
            {
                return NotFound();
            }

            _context.Follows.Remove(follow);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FollowExists(int id)
        {
            return _context.Follows.Any(e => e.Id == id);
        }
    }
}