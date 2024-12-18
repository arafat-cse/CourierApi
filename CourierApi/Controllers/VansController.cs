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
    public class VansController : ControllerBase
    {
        private readonly CourierDbContext _db;

        public VansController(CourierDbContext db)
        {
            _db = db;
        }

        // GET: api/Vans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Van>>> GetVans()
        {
            return await _db.Vans.ToListAsync();
        }

        // GET: api/Vans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Van>> GetVan(int id)
        {
            var van = await _db.Vans.FindAsync(id);

            if (van == null)
            {
                return NotFound();
            }

            return van;
        }

        // PUT: api/Vans/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVan(int id, Van van)
        {
            if (id != van.vanId)
            {
                return BadRequest();
            }

            _db.Entry(van).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VanExists(id))
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

        // POST: api/Vans
        [HttpPost]
        public async Task<ActionResult<Van>> PostVan(Van van)
        {
            _db.Vans.Add(van);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetVan", new { id = van.vanId }, van);
        }

        // DELETE: api/Vans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVan(int id)
        {
            var van = await _db.Vans.FindAsync(id);
            if (van == null)
            {
                return NotFound();
            }

            _db.Vans.Remove(van);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool VanExists(int id)
        {
            return _db.Vans.Any(e => e.vanId == id);
        }
    }
}
