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
    public class DesignationsController : ControllerBase
    {
        private readonly CourierDbContext _db;

        public DesignationsController(CourierDbContext db)
        {
            _db = db;
        }
        // GET: api/Designations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Designation>>> Getdesignations()
        {
            return await _db.designations.ToListAsync();
        }
        // GET: api/Designations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Designation>> GetDesignation(int id)
        {
            var designation = await _db.designations.FindAsync(id);

            if (designation == null)
            {
                return NotFound();
            }

            return designation;
        }
        // PUT: api/Designations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDesignation(int id, Designation designation)
        {
            if (id != designation.designationId)
            {
                return BadRequest();
            }

            _db.Entry(designation).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DesignationExists(id))
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
        // POST: api/Designations
        [HttpPost]
        public async Task<ActionResult<Designation>> PostDesignation(Designation designation)
        {
            _db.designations.Add(designation);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetDesignation", new { id = designation.designationId }, designation);
        }
        // DELETE: api/Designations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDesignation(int id)
        {
            var designation = await _db.designations.FindAsync(id);
            if (designation == null)
            {
                return NotFound();
            }

            _db.designations.Remove(designation);
            await _db.SaveChangesAsync();

            return NoContent();
        }
        private bool DesignationExists(int id)
        {
            return _db.designations.Any(e => e.designationId == id);
        }
    }
}
