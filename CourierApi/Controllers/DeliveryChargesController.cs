using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // Common response model
        private readonly CommanResponse cp = new CommanResponse();

        // GET:
        [HttpGet]
        public async Task<IActionResult> GetDeliveryCharges()
        {
            try
            {
                var deliveryCharges = await _db.DeliveryCharges.Include(dc => dc.ParcelTypes).Include(dc => dc.Parcels).ToListAsync();
                if (!deliveryCharges.Any())
                {
                    cp.status = false;
                    cp.message = "No delivery charges found.";
                    cp.content = null;
                    return Ok(cp);
                }

                cp.status = true;
                cp.message = "Delivery charges retrieved successfully.";
                cp.content = deliveryCharges;
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.status = false;
                cp.message = "Error occurred while retrieving delivery charges.";
                cp.errorMessage = ex.Message;
                cp.content = null;
                return StatusCode(500, cp);
            }
        }

        // GET:/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeliveryCharge(int id)
        {
            try
            {
                var deliveryCharge = await _db.DeliveryCharges.Include(dc => dc.ParcelTypes).Include(dc => dc.Parcels).FirstOrDefaultAsync(dc => dc.deliveryChargeId == id);

                if (deliveryCharge == null)
                {
                    cp.status = false;
                    cp.message = "Delivery charge not found.";
                    cp.content = null;
                    return NotFound(cp);
                }

                cp.status = true;
                cp.message = "Delivery charge retrieved successfully.";
                cp.content = deliveryCharge;
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.status = false;
                cp.message = "Error occurred while retrieving the delivery charge.";
                cp.errorMessage = ex.Message;
                cp.content = null;
                return StatusCode(500, cp);
            }
        }

        // POST:
        [HttpPost]
        public async Task<IActionResult> PostDeliveryCharge(DeliveryCharge deliveryCharge)
        {
            try
            {
                deliveryCharge.createDate = DateTime.UtcNow;
                deliveryCharge.IsActive = true;
                _db.DeliveryCharges.Add(deliveryCharge);
                await _db.SaveChangesAsync();

                cp.status = true;
                cp.message = "Delivery charge created successfully.";
                cp.content = deliveryCharge;
                return CreatedAtAction(nameof(GetDeliveryCharge), new { id = deliveryCharge.deliveryChargeId }, cp);
            }
            catch (Exception ex)
            {
                cp.status = false;
                cp.message = "Error occurred while creating delivery charge.";
                cp.errorMessage = ex.Message;
                cp.content = null;
                return BadRequest(cp);
            }
        }

        // PUT:
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeliveryCharge(int id, DeliveryCharge deliveryCharge)
        {
            if (id != deliveryCharge.deliveryChargeId)
            {
                cp.status = false;
                cp.message = "ID mismatch.";
                cp.content = null;
                return BadRequest(cp);
            }

            try
            {
                var existingCharge = await _db.DeliveryCharges.FindAsync(id);
                if (existingCharge == null)
                {
                    cp.status = false;
                    cp.message = "Delivery charge not found.";
                    cp.content = null;
                    return NotFound(cp);
                }
                existingCharge.weight = deliveryCharge.weight;
                existingCharge.price = deliveryCharge.price;
                existingCharge.updateBy = deliveryCharge.updateBy;
                existingCharge.updateDate = DateTime.UtcNow;
                existingCharge.parcelTypeId = deliveryCharge.parcelTypeId;

                _db.Entry(existingCharge).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                cp.status = true;
                cp.message = "Delivery charge updated successfully.";
                cp.content = existingCharge;
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.status = false;
                cp.message = "Error occurred while updating delivery charge.";
                cp.errorMessage = ex.Message;
                cp.content = null;
                return StatusCode(500, cp);
            }
        }

        // DELETE:
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeliveryCharge(int id)
        {
            try
            {
                var deliveryCharge = await _db.DeliveryCharges.FindAsync(id);
                if (deliveryCharge == null)
                {
                    cp.status = false;
                    cp.message = "Delivery charge not found.";
                    cp.content = null;
                    return NotFound(cp);
                }

                _db.DeliveryCharges.Remove(deliveryCharge);
                await _db.SaveChangesAsync();

                cp.status = true;
                cp.message = "Delivery charge deleted successfully.";
                cp.content = null;
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.status = false;
                cp.message = "Error occurred while deleting delivery charge.";
                cp.errorMessage = ex.Message;
                cp.content = null;
                return StatusCode(500, cp);
            }
        }

        private bool DeliveryChargeExists(int id)
        {
            return _db.DeliveryCharges.Any(e => e.deliveryChargeId == id);
        }
    }
}
