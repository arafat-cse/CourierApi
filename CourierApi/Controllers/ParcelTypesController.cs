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
        private readonly CourierDbContext _context;

        public ParcelTypesController(CourierDbContext context)
        {
            _context = context;
        }

        // GET: api/ParcelTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParcelType>>> GetParsersTypes()
        {
            return await _context.ParsersTypes.ToListAsync();
        }

        // GET: api/ParcelTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParcelType>> GetParcelType(int id)
        {
            var parcelType = await _context.ParsersTypes.FindAsync(id);

            if (parcelType == null)
            {
                return NotFound();
            }

            return parcelType;
        }

        // PUT: api/ParcelTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParcelType(int id, ParcelType parcelType)
        {
            if (id != parcelType.parcelTypeId)
            {
                return BadRequest();
            }

            _context.Entry(parcelType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ParcelType>> PostParcelType(ParcelType parcelType)
        {
            _context.ParsersTypes.Add(parcelType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParcelType", new { id = parcelType.parcelTypeId }, parcelType);
        }

        // DELETE: api/ParcelTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParcelType(int id)
        {
            var parcelType = await _context.ParsersTypes.FindAsync(id);
            if (parcelType == null)
            {
                return NotFound();
            }

            _context.ParsersTypes.Remove(parcelType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParcelTypeExists(int id)
        {
            return _context.ParsersTypes.Any(e => e.parcelTypeId == id);
        }
    }
}
