using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourierApi.Data;
using CourierApi.Models;
using System.Linq.Expressions;

namespace CourierApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelTypesController : ControllerBase
    {
        private readonly CourierDbContext _db;
        public ParcelTypesController(CourierDbContext db)
        {
            _db = db;
        }
        private readonly CommanResponse cp = new CommanResponse();
        // GET: api/ParcelTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParcelType>>> GetParsersTypes()
        {
            try
            {
                var perceltype = await _db.ParcelTypes.ToListAsync();
                if (perceltype == null || !perceltype.Any())
                {
                    cp.errorMessage = null;
                    cp.status = true;
                    cp.message = "No PerceType Found Here .";
                    cp.content = null;
                    return Ok(cp);
                }
                cp.errorMessage = null;
                cp.status = true;
                cp.message = "Type retrieved successfully!";
                cp.content = perceltype;
                return Ok(cp);
            }
            catch (Exception ex) 
            { 
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while retrieving the Customer.";
                cp.content = null;
                return BadRequest(cp);
            }
        }
        // GET: api/ParcelTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParcelType>> GetParcelType(int id)
        {
            try
            {
                var perceltype = await _db.ParcelTypes.FindAsync(id);
                if (perceltype == null)
                {
                    cp.errorMessage = "perceltype is not found";
                    cp.status = false;
                    cp.message = "No perceltype exists with the provided ID.";
                    cp.content = null;
                    return NotFound(cp);
                }
                cp.errorMessage = null;
                cp.status = true;
                cp.message = "Perceltype retrieved successfully!";
                cp.content = perceltype;
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while retrieving the Perceltype.";
                cp.content = null;
                return BadRequest(cp);
            }
        }
        // PUT: api/ParcelTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParcelType(int id, ParcelType parcelType)
        {
            if (id != parcelType.parcelTypeId)
            {
                cp.errorMessage = "Bad request ID mismatch";
                cp.status = false;
                cp.message = "Perceltype not found";
                cp.content = null;
                return BadRequest(cp);
            }
            _db.Entry(parcelType).State = EntityState.Modified;
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_db.ParcelTypes.Any(c => c.parcelTypeId == id))
                {
                    return NotFound("perceltype not found");
                }
                else
                {
                    throw;
                }
            }
            return Ok(new { Message = "Perceltype updated successfully", customerId = id });
        }
        // POST: api/ParcelTypes 
        [HttpPost]
        public async Task<ActionResult<ParcelType>> PostParcelType(ParcelType parcelType)
        {
            if (!ModelState.IsValid)
            {
                cp.errorMessage = "Invalid model state.";
                cp.status = false;
                cp.message = "Please check the input data.";
                cp.content = ModelState;
                return BadRequest(cp);
            }
            try
            {
                _db.ParcelTypes.Add(parcelType);
                await _db.SaveChangesAsync();
                cp.errorMessage = null;
                cp.status = true;
                cp.message = "Parcel type created successfully!";
                cp.content = parcelType;

                return CreatedAtAction("GetParcelType", new { id = parcelType.parcelTypeId }, cp);
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while creating the parcel type.";
                cp.content = null;
                return BadRequest(cp);
            }
        }
        // DELETE: api/ParcelTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParcelType(int id)
        {
            try
            {
                var perceltype = await _db.ParcelTypes.FindAsync(id);
                if (perceltype == null)
                {
                    cp.errorMessage = "perceltype is not found";
                    cp.status = false;
                    cp.message = "No perceltype exists with the provided ID.";
                    cp.content = null;
                    return NotFound(cp);
                }
                _db.ParcelTypes.Remove(perceltype);
                await _db.SaveChangesAsync();
                cp.errorMessage = null;
                cp.status = true;
                cp.message = "perceltype deleted successfully!";
                cp.content = perceltype;
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while deleting the perceltpe.";
                cp.content = null;
                return BadRequest(cp);
            }
        }
        private bool ParcelTypeExists(int id)
        {
            return _db.ParcelTypes.Any(e => e.parcelTypeId == id);
        }
    }
}
