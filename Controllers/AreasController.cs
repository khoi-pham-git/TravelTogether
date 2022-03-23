using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TravelTogether2.Common;
using TravelTogether2.Models;
using TravelTogether2.Services;

namespace TravelTogether2.Controllers
{

    [Route("api/v1.0/areas")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private readonly TourGuide_v2Context _context;
        private readonly IAreasResponsitory _areasResponsitory;
        private static string apiKey = "AIzaSyDYNx2dwpDfuTGnqF6Zip3uHVQTJCAFBqk";
        private static string apibucket = "traveltogether-54339.appspot.com";
        private static string authenEmail = "luanhua888@gmail.com";
        private static string authenPassword = "Luanhua123"; 


        public AreasController(TourGuide_v2Context context, IAreasResponsitory areasResponsitory)
        {
            _context = context;
            _areasResponsitory = areasResponsitory;
        }

        // GET: api/Areas
        // Get list area information - Luan
        /// <summary>
        /// Get list all Area with 
        /// </summary>                                                                                                                                //Luân
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Area>>> GetAreas(string search, string sortby, int page = 1)
        {
            try
            {

                var result = _areasResponsitory.GetAll(search, sortby, page);
                var result1 = await (from c in _context.Areas
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

        //// GET: api/Areas/5
        ///// <summary>
        ///// Get area by id
        ///// </summary>
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Area>> GetArea(int id)
        //{
        //    try
        //    {
        //        var result = await (from area in _context.Areas
        //                            where area.Id == id
        //                            select new
        //                            {
        //                                Id = area.Id,
        //                                Name = area.Name,
        //                                Description = area.Description,
        //                                Latitude = area.Latitude,
        //                                Longtitude = area.Longtitude
        //                                //Khóa ngoại travel agenciesid
        //                            }
        //                        ).ToListAsync();
        //        if (!result.Any())
        //        {
        //            return BadRequest(new { StatusCode = 404, message = "ID is not found!" });
        //        }
        //        return Ok(new { StatusCodes = 200, message = "The request was successfully completed", data = result });

        //    }
        //    catch (Exception e)
        //    {

        //        return StatusCode(409, new { StatusCode = 409, message = e.Message });
        //    }
        //}

        // PUT: api/Areas/5 Luan
        /// <summary>
        /// Edit area by id
        /// </summary>
        ///                                                           
                                                                                                                                //Luân
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArea(int id, Area area)
        {
            try
            {
                var area1 = _context.Areas.Find(id);
                var travelagencyid = new TravelAgency();
                if (!AreaExists(area.Id = id))
                {
                    return BadRequest(new { StatusCode = 404, Message = "ID Not Found!" });
                }

                if (!Validate.isName(area1.Name = area.Name))
                {
                    return BadRequest(new { StatusCode = 404, Message = "only character!" });
                }
                area1.Description = area.Description;
                area1.Latitude = area.Latitude;
                area1.Longtitude = area.Longtitude;
                //area1.TravelAgency=area.TravelAgency;
                var travelagencyid1 = _context.TravelAgencies.FirstOrDefault(s => s.Id == area.TravelAgencyId);
                if (travelagencyid1 == null)
                {
                    await _context.SaveChangesAsync();
                    return BadRequest(new { StatusCode = 404, Message = "Travel agency not found!!" });
                }
                return Ok(new { status = 200, message = "Update Successful!" });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, message = e.Message });
            }
        }

        // POST: api/Areas
        /// <summary>
        /// Create area by id
        /// </summary>
        [HttpPost]                                                                                                                                //Luân

        public async Task<ActionResult<Area>> PostArea(Area area)
        {
            try
            {
                var area1 = new Area();
                var travelagencyid = new TravelAgency();

                if (!Validate.isName(area1.Name = area.Name))
                {
                    return BadRequest(new { StatusCode = 404, Message = "only character!" });
                }
                area1.Description = area.Description;
                area1.Latitude = area.Latitude;
                area1.Longtitude = area.Longtitude;
                area1.TravelAgencyId = area.TravelAgencyId;
                var travelagencyid1 = _context.TravelAgencies.FirstOrDefault(s => s.Id == area.TravelAgencyId);
                if (travelagencyid1 == null)
                {
                    return BadRequest(new { StatusCode = 404, Message = "Travel agency not found" });

                }

                _context.Areas.Add(area1);
                await _context.SaveChangesAsync();

                return Ok(new { status = 201, message = "Create area successfull!" });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, message = e.Message });
            }
        }

        // DELETE: api/Areas/5
        /// <summary>
        /// Delete area by id (not use)
        /// </summary>
        [HttpDelete("{id}")]                                                                                                                                //Luân
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






        /// <summary>
        /// Create firebase
        /// </summary>
        [HttpPost("UploadFile")]                                                                                                                                //Luân

        public async Task<ActionResult> PostFireBase(IFormFile file)
        {
            try
            {
                var fileUpload = file;
                FileStream fs = null;
                if (fileUpload.Length > 0)
                {
                    {
                        string folderName = "ImagesFile";
                        string path = Path.Combine($"Image", $"Image/{folderName}");
                        if (Directory.Exists(path))
                        {
                            using (fs = new FileStream(Path.Combine(path, fileUpload.FileName), FileMode.Create))
                            {
                                await fileUpload.CopyToAsync(fs);
                            }
                            fs = new FileStream(Path.Combine(path, fileUpload.FileName), FileMode.Open);
                        }
                        else
                        {
                            Directory.CreateDirectory(path);
                        }

                    }

                    var authen = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
                    var a = await authen.SignInWithEmailAndPasswordAsync(authenEmail, authenPassword);
                    var cancel = new CancellationTokenSource();
                    var upload = new FirebaseStorage(
                        apibucket,
                        new FirebaseStorageOptions
                        {
                            AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                            ThrowOnCancel = true
                        }
                        ).Child("Image").Child(fileUpload.FileName).PutAsync(fs, cancel.Token);
                    try
                    {
                        string Link = await upload;
                        return Ok(new {StatusCode = 200 , message ="Upload file succesful!"});
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }

                return BadRequest("Upload  fail");

            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, message = e.Message });
            }
        }



        private bool AreaExists(int id)
        {
            return _context.Areas.Any(e => e.Id == id);
        }
    }
}
