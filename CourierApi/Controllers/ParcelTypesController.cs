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
    public class ParcelTypesController : ControllerBase
    {
        private readonly CourierDbContext _db;

        public ParcelTypesController(CourierDbContext db)
        {
            _db = db;
        }

        // GET: api/ParcelTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParcelType>>> GetParsersTypes()
        {
            return await _db.ParsersTypes.ToListAsync();
        }

        // GET: api/ParcelTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParcelType>> GetParcelType(int id)
        {
            var parcelType = await _db.ParsersTypes.FindAsync(id);

            if (parcelType == null)
            {
                return NotFound();
            }

            return parcelType;
        }

        // PUT: api/ParcelTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParcelType(int id, ParcelType parcelType)
        {
            if (id != parcelType.parcelTypeId)
            {
                return BadRequest();
            }

            _db.Entry(parcelType).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParcelTypeExists(id))
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

        // POST: api/ParcelTypes 
        [HttpPost]
        public async Task<ActionResult<ParcelType>> PostParcelType(ParcelType parcelType)
        {
            _db.ParsersTypes.Add(parcelType);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetParcelType", new { id = parcelType.parcelTypeId }, parcelType);
        }

        // DELETE: api/ParcelTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParcelType(int id)
        {
            var parcelType = await _db.ParsersTypes.FindAsync(id);
            if (parcelType == null)
            {
                return NotFound();
            }

            _db.ParsersTypes.Remove(parcelType);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool ParcelTypeExists(int id)
        {
            return _db.ParsersTypes.Any(e => e.parcelTypeId == id);
        }
    }
}
