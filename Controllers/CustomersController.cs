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
    [Route("api/v1.0/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly TourGuide_v2Context _context;
        private readonly ICustomerRespository _customerRespository;


        public CustomersController(TourGuide_v2Context context, ICustomerRespository customerRespository)
        {
            _context = context;
            _customerRespository = customerRespository;

        }

        // GET: api/Customers
        //gett list customer                                                                                                                                //Luân
        /// <summary>
        /// Get all Customer
        /// </summary>
        //Phan trang
        [HttpGet("customers")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers(string search, string sortby, int page = 1)
        {
            try
            {
                var result = _customerRespository.GetAll(search, sortby, page);
                var result1 = await (from c in _context.Customers
                                     select new
                                     {
                                         c.Id
                                     }).ToListAsync();

                return Ok(new { StatusCodes = 200, message = "The request was successfully completed", data = result });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, message = e.Message });
            }
        }

        // GET: api/Customers/5
        /// <summary>                                                                                                                                //Luân
        /// Get Customer by id
        /// </summary>
        [HttpGet("customers/id")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            try
            {
                var result = await (from Customer in _context.Customers
                                    where Customer.Id == id
                                    select new Customer
                                    {
                                        Id = Customer.Id,
                                        Name = Customer.Name,
                                        Phone = Customer.Phone,
                                        Email = Customer.Email,
                                        Address = Customer.Address,
                                        Image = Customer.Image
                                    }
                                    ).ToListAsync();
                return Ok(new { StatusCodes = 200, message = "The request was successfully completed", data = result });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, message = e.Message });
            }
        }




        // PUT: api/Customers/5

        /// <summary>
        /// Edit Customer by                                                                                                                                 //Luân
        /// </summary>
        [HttpPut("customers/{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            try
            {
                var cus1 = _context.Customers.Find(id);
                cus1.Address = customer.Address;
                cus1.Image = customer.Image;
                if (!CustomerExists(cus1.Id = id))
                {
                    return BadRequest(new { StatusCode = 404, Message = "Id not found!" });
                }
                else if (!Validate.isName(cus1.Name = customer.Name))
                {
                    return BadRequest(new { StatusCode = 400, Message = "Only character!" });
                }
                else if (!Validate.isPhone(cus1.Phone = customer.Phone))
                {
                    return BadRequest(new { StatusCode = 400, Message = "Invalid phone number" });
                }
                else if (!Validate.isEmail(cus1.Email = customer.Email))
                {
                    return BadRequest(new { StatusCode = 400, Message = "Invalid email!" });
                }
                else
                {
                    await _context.SaveChangesAsync();
                    return Ok(new { status = 200, message = "Update Successful!" });

                }
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, message = e.Message });
            }
        }






        // PUT: api/Customers/5

        ///// <summary>                                                                                                                                //Luân
        ///// Edit Customer by id
        ///// </summary>
        //[HttpPut("follow/{id}")]
        //public async Task<IActionResult> PutFollows(int id)    //id tourguide
        //{
        //    try
        //    {




        //        //if (tourguideid == null)
        //        //{
        //        //    return BadRequest(new { StatusCode = 404, Message = "Tourgide id is not found!" });
        //        //}

        //        //if (follow.Status == true)
        //        //{
        //        //    follow.Status = false;
        //        //    return Ok(new { status = 200, message = "Unfollow" });
        //        //}
        //        //else
        //        //{
        //        //    //follow.Status = true;                                                                                                                                //Luân
        //        //    //await _context.SaveChangesAsync();
        //        return Ok(new { status = 200, message = "Follow" });

        //        //}
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(409, new { StatusCode = 409, message = e.Message });
        //    }
        //}

        // POST: api/Customers

        /// <summary>
        /// Create Customer
        /// </summary>
        /// /// <remarks>
        /// Sample value of message
        /// 
        ///     POST /Todo
        ///     {
        ///        "name": "Sukkh",
        ///        "phone": "0961449382",
        ///        "email": "Sukhpinder@gmail.com",
        ///        "address": "Kha Van Can",
        ///        "image": "Sukkh.png"
        ///     }
        ///     
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            //_context.Customers.Add(customer);
            try
            {
                var cus1 = new Customer();                                                                                                                                //Luân
                cus1.Address = customer.Address;
                //cus1.Image = customer.Image;
                if (!Validate.isName(cus1.Name = customer.Name))
                {
                    return BadRequest(new { StatusCode = 400, Message = "Only character!" });
                }
                else if (!Validate.isPhone(cus1.Phone = customer.Phone))
                {
                    return BadRequest(new { StatusCode = 400, Message = "Invalid phone number" });
                }
                else if (!Validate.isEmail(cus1.Email = customer.Email))
                {
                    return BadRequest(new { StatusCode = 400, Message = "Invalid email!" });
                }
                else
                {
                    _context.Customers.Add(cus1);
                    await _context.SaveChangesAsync();
                    return Ok(new { status = 201, message = "Create Customer Successful!" });
                }
            }
            catch (Exception e)
            {

                return StatusCode(409, new { StatusCode = 409, message = e.Message });
            }
        }

        // DELETE: api/Customers/5
        /// <summary>
        /// Delete Customer by id (not use)
        /// </summary>                                                                                                                                //Luân
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
