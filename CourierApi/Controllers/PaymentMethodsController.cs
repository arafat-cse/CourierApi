using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourierApi.Data;
using CourierApi.Models;

namespace CourierApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodsController : ControllerBase
    {
        private readonly CourierDbContext _db;

        public PaymentMethodsController(CourierDbContext db)
        {
            _db = db;
        }
        //CommanResponse
        private readonly CommanResponse cp = new CommanResponse();
        // GET: 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentMethod>>> GetPaymentMethods()
        {
            try
            {
                var paymentMethod = await _db.PaymentMethods.ToListAsync();
                if (paymentMethod == null || !paymentMethod.Any())
                {
                    cp.errorMessage = null;
                    cp.status = true;
                    cp.message = "No paymentMethod found.";
                    cp.content = null;
                    return Ok(cp);
                }
                cp.errorMessage = null;
                cp.status = true;
                cp.message = "PaymentMethod retrieved successfully!";
                cp.content = paymentMethod;
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while retrieving the PaymentMethod.";
                cp.content = null;
                return BadRequest(cp);
            }
        }

        // GET: /5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentMethod>> GetPaymentMethod(int id)
        {
            try
            {
                var paymentMethod = await _db.PaymentMethods.FindAsync(id);
                if (paymentMethod == null)
                {
                    cp.errorMessage = "paymentMethod not found";
                    cp.status = false;
                    cp.message = "No PaymentMethod exists with the provided ID.";
                    cp.content = null;
                    return NotFound(cp);
                }
                cp.errorMessage = null;
                cp.status = true;
                cp.message = "PaymentMethod retrieved successfully!";
                cp.content = paymentMethod;
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while retrieving the PaymentMethod.";
                cp.content = null;
                return BadRequest(cp);
            }
        }

        // PUT: /5       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentMethod(int id, PaymentMethod paymentMethod)
        {
            if (id != paymentMethod.paymentMethodId)
            {
                cp.errorMessage = "Badrequer ID mismatch";
                cp.status = false;
                cp.message = "paymentMethod not found";
                cp.content = null;
                return BadRequest(cp);
            }
            _db.Entry(paymentMethod).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentMethodExists(id))
                {
                    return NotFound("paymentMethod not found");
                }
                else
                {
                    throw;
                }
            }
            return Ok(new { Message = "paymentMethod updated successfully", paymentMethodId = id });
        }

        // POST: 
        [HttpPost]
        public async Task<ActionResult<PaymentMethod>> PostPaymentMethod(PaymentMethod paymentMethod)
        {
            try
            {
                _db.PaymentMethods.Add(paymentMethod);
                await _db.SaveChangesAsync();

                cp.errorMessage = null;
                cp.status = true;
                cp.message = "paymentMethod created successfully!";
                cp.content = paymentMethod;
                return CreatedAtAction("GetpaymentMethod", new { id = paymentMethod.paymentMethodId }, paymentMethod);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "Failed to paymentMethod.";
                cp.content = null;
                return BadRequest(cp);
            }
        }

        // DELETE: /5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentMethod(int id)
        {
            try
            {
                // Find the company by ID
                var paymentMethod = await _db.PaymentMethods.FindAsync(id);

                if (paymentMethod == null)
                {
                    cp.errorMessage = "paymentMethod not found";
                    cp.status = false;
                    cp.message = "No paymentMethod exists with the provided ID.";
                    cp.content = null;
                    return NotFound(cp);
                }
                _db.PaymentMethods.Remove(paymentMethod);
                await _db.SaveChangesAsync();
                cp.errorMessage = null;
                cp.status = true;
                cp.message = "paymentMethod deleted successfully!";
                cp.content = paymentMethod;
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while deleting the paymentMethod.";
                cp.content = null;
                return BadRequest(cp);
            }
        }
        private bool PaymentMethodExists(int id)
        {
            return _db.PaymentMethods.Any(e => e.paymentMethodId == id);
        }
    }
}
