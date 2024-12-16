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
        public async Task<ActionResult<IEnumerable<Parcel>>> GetParcels()
        {
            try
            {
                var parcels = await _db.Parsers
                    .Include(p => p.Branchs)
                    .Include(p => p.ParcelTypes)
                    .Include(p => p.DeliveryCharges)
                    .Include(p => p.Vans)
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
                cp.content = null;
                return StatusCode(500, cp);
            }
        }

        // GET: api/Parcels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Parcel>> GetParcel(int id)
        {
            try
            {
                var parcel = await _db.Parsers
                    .Include(p => p.Branchs)
                    .Include(p => p.ParcelTypes)
                    .Include(p => p.DeliveryCharges)
                    .Include(p => p.Vans)
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
                cp.content = null;
                return StatusCode(500, cp);
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
                var existingParcel = await _db.Parsers.FindAsync(id);
                if (existingParcel == null)
                {
                    return NotFound("Parcel not found.");
                }

                existingParcel.parcelCode = parcel.parcelCode;
                existingParcel.senderCustomerId = parcel.senderCustomerId;
                existingParcel.receiverCustomerId = parcel.receiverCustomerId;
                existingParcel.price = parcel.price;
                existingParcel.updateBy = parcel.updateBy;
                existingParcel.updateDate = DateTime.UtcNow;

                _db.Entry(existingParcel).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParcelExists(id))
                {
                    return NotFound("Parcel not found.");
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

        // POST: api/Parcels
        [HttpPost]
        public async Task<ActionResult<Parcel>> PostParcel(Parcel parcel)
        {
            try
            {
                parcel.createDate = DateTime.UtcNow;
                parcel.IsActive = true;
                _db.Parsers.Add(parcel);
                await _db.SaveChangesAsync();

                cp.status = true;
                cp.message = "Parcel created successfully.";
                cp.content = parcel;
                return CreatedAtAction(nameof(GetParcel), new { id = parcel.parcelId }, cp);
            }
            catch (Exception ex)
            {
                cp.status = false;
                cp.message = "Error occurred while creating the parcel.";
                cp.errorMessage = ex.Message;
                return BadRequest(cp);
            }
        }

        // DELETE: api/Parcels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParcel(int id)
        {
            try
            {
                var parcel = await _db.Parsers.FindAsync(id);
                if (parcel == null)
                {
                    cp.status = false;
                    cp.message = "Parcel not found.";
                    return NotFound(cp);
                }

                _db.Parsers.Remove(parcel);
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
            return _db.Parsers.Any(p => p.parcelId == id);
        }
    }
}
