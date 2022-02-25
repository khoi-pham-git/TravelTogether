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
    //[Route("api/[controller]")]
    [Route("api/v1.0/categories")]

    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly TourGuide_v2Context _context;

        public CategoriesController(TourGuide_v2Context context)
        {
            _context = context;
        }

        // GET: api/Categories
        // Get All không phân trang - Luan
        /// <summary>
        /// Get all Category 
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            try
            {
                var result = await (from Categories in _context.Categories
                                    select new
                                    {
                                        Categories.Id,
                                        Categories.Name
                                    }
                                     ).ToListAsync();
                return Ok(new { StatusCodes = 200, message = "The request was successfully completed", data = result });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, message = e.Message });
            }
        }

        // GET: api/Categories/5
        //find by ID -Luan
        /// <summary>
        /// Get Category  by id
        /// </summary>
        [HttpGet("id")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            try
            {
                var result = await (from Categories in _context.Categories
                                    where Categories.Id == id
                                    select new
                                    {
                                        Categories.Id,
                                        Categories.Name
                                    }
                                     ).ToListAsync();

                if (!result.Any())
                {
                    return BadRequest(new { StatusCode = 404, message = "ID is not found!" });
                }

                return Ok(new { StatusCodes = 200, message = "The request was successfully completed", data = result });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, message = e.Message });
            }
        }


        // GET: api/Categories/5
        //Find by Name- Luan
        /// <summary>
        /// Get Category by name 
        /// </summary>
        [HttpGet("name")]
        public async Task<ActionResult<Category>> GetCategorybyName(string name)
        {
            try
            {
                var result = await (from Categories in _context.Categories
                                    where Categories.Name.Contains(name) // tìm gần đúng
                                    select new
                                    {
                                        Categories.Id,
                                        Categories.Name
                                    }
                                     ).ToListAsync();

                if (!result.Any())
                {
                    return BadRequest(new { StatusCode = 404, message = "Name is not found!" });
                }

                return Ok(new { StatusCodes = 200, message = "The request was successfully completed", data = result });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, message = e.Message });
            }
        }

        // PUT: api/Categories/5
        /// <summary>
        /// Edit Category by id
        /// </summary>
        // Edit Categories
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            try
            {
                var category1 = _context.Categories.Find(id);
                if (!CategoryExists(category.Id = id))
                {
                    return BadRequest(new { StatusCode = 404, Message = "ID Not Found!" });
                }

                if (!Validate.isName(category1.Name = category.Name))
                {
                    return BadRequest(new { StatusCode = 404, Message = "only character!" });
                }
                else
                {
                    await _context.SaveChangesAsync();
                    return Ok(new { status = 200, message = "oke update rồi được chưa" });

                }
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, message = e.Message });
            }
        }

        // POST: api/Categories
        /// <summary>
        /// Create Category
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            try
            {
                var category1 = new Category();
                if (!Validate.isName(category1.Name = category.Name))
                {
                    return BadRequest(new { StatusCode = 404, Message = "only character!" });
                }
                else
                {
                    _context.Categories.Add(category1);
                    await _context.SaveChangesAsync();
                    return Ok(new { status = 201, message = "Create category successfull!" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, message = e.Message });
            }
        }

        // DELETE: api/Categories/5
        /// <summary>
        /// Delete Category by id (not use)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
