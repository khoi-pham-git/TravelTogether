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
    [Route("api/v1.0/payment")]

    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly TourGuide_v2Context _context;

        public PaymentsController(TourGuide_v2Context context)
        {
            _context = context;
        }

        // GET: api/Payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments(int ele, int page)
        {
            try
            {
                var result = await (from payment in _context.Payments
                                    select new
                                    {
                                        payment.Id,
                                        payment.Amount,
                                        payment.Type,
                                        payment.Date,
                                        payment.TripId,
                                        payment.PaymentCode

                                    }
                                    ).ToListAsync();

                int totalEle = result.Count;
                int totalPage = Validate.totalPage(totalEle, ele);
                result = result.Skip((page - 1) * ele).Take(ele).ToList();

                return Ok(new { StatusCode = 200, message = "The request was successfully completed", data = result, totalEle, totalPage });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(int id)
        {
            try
            {
                var result = await (from payment in _context.Payments
                                    where payment.Id == id
                                    select new
                                    {
                                        payment.Id,
                                        payment.Amount,
                                        payment.Type,
                                        payment.Date,
                                        payment.TripId,
                                        payment.PaymentCode

                                    }
                                    ).ToListAsync();



                return Ok(new { StatusCode = 200, message = "The request was successfully completed", data = result });
            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // PUT: api/Payments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(int id, Payment payment)
        {
            try
            {
                var payment1 = _context.Payments.Find(id);
                payment1.Amount = payment.Amount;
                payment1.Type = payment.Type;
                payment1.Date = payment.Date;
                payment1.TripId = payment.TripId;
                payment1.PaymentCode = payment.PaymentCode;

                if (!PaymentExists(payment.Id = id))
                {
                    return BadRequest(new { StatusCode = 404, Message = "ID Not Found!" });
                }
                else
                {
                    await _context.SaveChangesAsync();
                    return Ok(new { status = 200, message = "Update Successful!" });
                }

            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // POST: api/Payments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Payment>> PostPayment(Payment payment)
        {
            try
            {
                var payment1 = new Payment();
                payment1.Amount = payment.Amount;
                payment1.Type = payment.Type;
                payment1.Date = payment.Date;
                payment1.TripId = payment.TripId;
                payment1.PaymentCode = payment.PaymentCode;

                _context.Payments.Add(payment1);
                await _context.SaveChangesAsync();
                return Ok(new { status = 200, message = "Update Successful!" });


            }
            catch (Exception e)
            {
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.Id == id);
        }
    }
}
