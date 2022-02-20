﻿using System;
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
    public class CustomersController : ControllerBase
    {
        private readonly TourGuide_v2Context _context;

        public CustomersController(TourGuide_v2Context context)
        {
            _context = context;
        }

        // GET: api/Customers
        //gett list customer
        //Phan trang
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers(int ele, int page)
        {
            try
            {
                var result = await (from Customer in _context.Customers
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

                int totalEle = result.Count;
                int totalPage = Validate.totalPage(totalEle, ele);
                result = result.Skip((page - 1) * ele).Take(ele).ToList();

                return Ok(new { StatusCodes = 200, message = "The request was successfully completed", data = result });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, message = e.Message });
            }
        }

        // GET: api/Customers/5
        [HttpGet("id")]
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


        // GET: api/Customers/5
        [HttpGet("name")]
        public async Task<ActionResult<Customer>> GetCustomerById(String name)
        {
            try
            {
                var result = await (from Customer in _context.Customers
                                    where Customer.Name.Contains(name) //Tìm gần đúng
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
        [HttpPut("{id}")]
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
                    return Ok(new { status = 200, message = "oke update rồi được chưa" });

                }
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, message = e.Message });
            }
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            //_context.Customers.Add(customer);
            try
            {
                var cus1 = new Customer();
                cus1.Address = customer.Address;
                cus1.Image = customer.Image;
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
