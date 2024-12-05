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
        private readonly CourierDbContext _context;

        public ParcelsController(CourierDbContext context)
        {
            _context = context;
        }

        // GET: api/Parcels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parcel>>> GetParsers()
        {
            return await _context.Parsers.ToListAsync();
        }

        // GET: api/Parcels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Parcel>> GetParcel(int id)
        {
            var parcel = await _context.Parsers.FindAsync(id);

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
            if (id != parcel.ParcelId)
            {
                return BadRequest();
            }

            _context.Entry(parcel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
            _context.Parsers.Add(parcel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParcel", new { id = parcel.ParcelId }, parcel);
        }

        // DELETE: api/Parcels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParcel(int id)
        {
            var parcel = await _context.Parsers.FindAsync(id);
            if (parcel == null)
            {
                return NotFound();
            }

            _context.Parsers.Remove(parcel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParcelExists(int id)
        {
            return _context.Parsers.Any(e => e.ParcelId == id);
        }
    }
}
