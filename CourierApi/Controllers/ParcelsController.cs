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
                cp.content = null;
                return StatusCode(500, cp);
            }
        }

        // GET: api/Parcels/{id}
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
                cp.content = null;
                return StatusCode(500, cp);
            }
        }

        // POST: api/Parcels
        [HttpPost]
        public async Task<IActionResult> PostParcel(Parcel parcel)
        {
            try
            {
                if (parcel.senderBranchId == parcel.receiverBranchId)
                {
                    cp.status = false;
                    cp.message = "Sender and receiver branches cannot be the same.";
                    return BadRequest(cp);
                }

                _db.Parcels.Add(parcel);
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
                cp.content = null;
                return StatusCode(500, cp);
            }
        }

        // PUT: api/Parcels/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParcel(int id, Parcel parcel)
        {
            if (id != parcel.parcelId)
            {
                cp.status = false;
                cp.message = "Parcel ID mismatch.";
                return BadRequest(cp);
            }

            try
            {
                _db.Entry(parcel).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                cp.status = true;
                cp.message = "Parcel updated successfully.";
                cp.content = parcel;
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

        // DELETE: api/Parcels/{id}
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
