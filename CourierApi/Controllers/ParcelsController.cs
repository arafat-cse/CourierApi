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
    public class ParcelsController : ControllerBase
    {
        private readonly CourierDbContext _db;
        private readonly CommanResponse cp = new CommanResponse();

        public ParcelsController(CourierDbContext db)
        {
            _db = db;
        }

        // GET: api/Parcels
        [HttpGet]
        public async Task<IActionResult> GetParcels()
        {
            try
            {
                var parcels = await _db.Parcels
                    .Include(p => p.SenderBranch)
                    .Include(p => p.ReceiverBranch)
                    .Include(p => p.ParcelType)
                    .Include(p => p.DeliveryCharge)
                    .Include(p => p.Van)
                    .ToListAsync();

                if (!parcels.Any())
                {
                    cp.status = false;
                    cp.message = "No parcels found.";
                    cp.content = null;
                    return Ok(cp);
                }

                cp.status = true;
                cp.message = "Parcels retrieved successfully.";
                cp.content = parcels;
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.status = false;
                cp.message = "Error occurred while retrieving parcels.";
                cp.errorMessage = ex.Message;
                return StatusCode(500, cp);
            }
        }

        // GET: api/Parcels/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetParcel(int id)
        {
            try
            {
                var parcel = await _db.Parcels
                    .Include(p => p.SenderBranch)
                    .Include(p => p.ReceiverBranch)
                    .Include(p => p.ParcelType)
                    .Include(p => p.DeliveryCharge)
                    .Include(p => p.Van)
                    .FirstOrDefaultAsync(p => p.parcelId == id);

                if (parcel == null)
                {
                    cp.status = false;
                    cp.message = "Parcel not found.";
                    cp.content = null;
                    return NotFound(cp);
                }

                cp.status = true;
                cp.message = "Parcel retrieved successfully.";
                cp.content = parcel;
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.status = false;
                cp.message = "Error occurred while retrieving the parcel.";
                cp.errorMessage = ex.Message;
                return StatusCode(500, cp);
            }
        }

        // POST: api/Parcels
        [HttpPost]
        public async Task<IActionResult> PostParcel(Parcel parcel)
        {
            try
            {
                // Validate dependencies
                var isValidSenderBranch = await _db.Branches.AnyAsync(d => d.branchId == parcel.senderBranchId);
                var isValidReceiverBranch = await _db.Branches.AnyAsync(d => d.branchId == parcel.receiverBranchId);
                var isValidParcelType = await _db.ParcelTypes.AnyAsync(d => d.parcelTypeId == parcel.parcelTypeId);
                var isValidDeliveryCharge = await _db.DeliveryCharges.AnyAsync(d => d.deliveryChargeId == parcel.deliveryChargeId);
                var isValidVan = parcel.vanId == null || await _db.Vans.AnyAsync(d => d.vanId == parcel.vanId);

                if (!isValidSenderBranch || isValidReceiverBranch || !isValidParcelType || !isValidDeliveryCharge || !isValidVan)
                {
                    cp.errorMessage = "Invalid dependencies.";
                    cp.status = false;
                    cp.message = "One or more dependencies are invalid.";
                    cp.content = null;
                    return BadRequest(cp);
                }

                // Add new parcel
                _db.Parcels.Add(parcel);
                await _db.SaveChangesAsync();

                cp.status = true;
                cp.message = "Parcel created successfully.";
                cp.content = parcel;
                return CreatedAtAction(nameof(GetParcel), new { id = parcel.parcelId }, cp);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "Failed to create a new Parcel.";
                cp.content = null;
                return BadRequest(cp);
            }
        }

        // PUT: api/Parcels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParcel(int id, Parcel parcel)
        {
            if (id != parcel.parcelId)
            {
                return BadRequest("Parcel ID mismatch.");
            }

            try
            {
                var existingParcel = await _db.Parcels.FindAsync(id);
                if (existingParcel == null)
                {
                    cp.status = false;
                    cp.message = "Parcel not found.";
                    return NotFound(cp);
                }

                // Update fields
                existingParcel.parcelCode = parcel.parcelCode;
                existingParcel.senderCustomerId = parcel.senderCustomerId;
                existingParcel.receiverCustomerId = parcel.receiverCustomerId;
                existingParcel.price = parcel.price;
                existingParcel.updateBy = parcel.updateBy;
                existingParcel.updateDate = DateTime.UtcNow;

                _db.Entry(existingParcel).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                cp.status = true;
                cp.message = "Parcel updated successfully.";
                cp.content = existingParcel;
                return Ok(cp);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParcelExists(id))
                {
                    cp.status = false;
                    cp.message = "Parcel not found.";
                    return NotFound(cp);
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                cp.status = false;
                cp.message = "Error occurred while updating the parcel.";
                cp.errorMessage = ex.Message;
                return StatusCode(500, cp);
            }
        }

        // DELETE: api/Parcels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParcel(int id)
        {
            try
            {
                var parcel = await _db.Parcels.FindAsync(id);
                if (parcel == null)
                {
                    cp.status = false;
                    cp.message = "Parcel not found.";
                    return NotFound(cp);
                }

                _db.Parcels.Remove(parcel);
                await _db.SaveChangesAsync();

                cp.status = true;
                cp.message = "Parcel deleted successfully.";
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.status = false;
                cp.message = "Error occurred while deleting the parcel.";
                cp.errorMessage = ex.Message;
                return StatusCode(500, cp);
            }
        }

        private bool ParcelExists(int id)
        {
            return _db.Parcels.Any(p => p.parcelId == id);
        }
    }
}
