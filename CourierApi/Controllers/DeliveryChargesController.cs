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
    public class DeliveryChargesController : ControllerBase
    {
        private readonly CourierDbContext _db;

        public DeliveryChargesController(CourierDbContext db)
        {
            _db = db;
        }

        // GET: api/DeliveryCharges
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeliveryCharge>>> GetDeliveryCharges()
        {
            return await _db.DeliveryCharges.ToListAsync();
        }

        // GET: api/DeliveryCharges/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeliveryCharge>> GetDeliveryCharge(int id)
        {
            var deliveryCharge = await _db.DeliveryCharges.FindAsync(id);

            if (deliveryCharge == null)
            {
                return NotFound();
            }

            return deliveryCharge;
        }

        // PUT: api/DeliveryCharges/5      
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeliveryCharge(int id, DeliveryCharge deliveryCharge)
        {
            if (id != deliveryCharge.deliveryChargeId)
            {
                return BadRequest();
            }

            _db.Entry(deliveryCharge).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryChargeExists(id))
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

        // POST: api/DeliveryCharges
        [HttpPost]
        public async Task<ActionResult<DeliveryCharge>> PostDeliveryCharge(DeliveryCharge deliveryCharge)
        {
            _db.DeliveryCharges.Add(deliveryCharge);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetDeliveryCharge", new { id = deliveryCharge.deliveryChargeId }, deliveryCharge);
        }

        // DELETE: api/DeliveryCharges/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeliveryCharge(int id)
        {
            var deliveryCharge = await _db.DeliveryCharges.FindAsync(id);
            if (deliveryCharge == null)
            {
                return NotFound();
            }

            _db.DeliveryCharges.Remove(deliveryCharge);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool DeliveryChargeExists(int id)
        {
            return _db.DeliveryCharges.Any(e => e.deliveryChargeId == id);
        }
    }
}
