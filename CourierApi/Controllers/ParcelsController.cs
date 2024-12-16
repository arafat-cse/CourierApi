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
    public class ParcelsController : ControllerBase
    {
        private readonly CourierDbContext _db;

        public ParcelsController(CourierDbContext db)
        {
            _db = db;
        }

        // GET: api/Parcels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parcel>>> GetParsers()
        {
            return await _db.Parsers.ToListAsync();
        }

        // GET: api/Parcels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Parcel>> GetParcel(int id)
        {
            var parcel = await _db.Parsers.FindAsync(id);

            if (parcel == null)
            {
                return NotFound();
            }

            return parcel;
        }

        // PUT: api/Parcels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParcel(int id, Parcel parcel)
        {
            if (id != parcel.parcelId)
            {
                return BadRequest();
            }

            _db.Entry(parcel).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParcelExists(id))
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

        // POST: api/Parcels
        [HttpPost]
        public async Task<ActionResult<Parcel>> PostParcel(Parcel parcel)
        {
            _db.Parsers.Add(parcel);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetParcel", new { id = parcel.parcelId }, parcel);
        }

        // DELETE: api/Parcels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParcel(int id)
        {
            var parcel = await _db.Parsers.FindAsync(id);
            if (parcel == null)
            {
                return NotFound();
            }

            _db.Parsers.Remove(parcel);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool ParcelExists(int id)
        {
            return _db.Parsers.Any(e => e.parcelId == id);
        }
    }
}
