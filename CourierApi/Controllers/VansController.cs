using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourierApi.Data;
using CourierApi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Humanizer;

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
        // CommanResponse
        private readonly CommanResponse cp = new CommanResponse();
        // GET: 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Van>>> GetVans()
        {
            try
            {
                var Van = await _db.Vans.ToListAsync();
                if (Van == null || !Van.Any())
                {
                    cp.errorMessage = "No van found.";
                    cp.status = false;
                    cp.message = "No van data available.";
                    cp.content = null;
                    return Ok(cp);
                }
                cp.errorMessage = null;
                cp.status = true;
                cp.message = "van retrieved successfully!";
                cp.content = Van;
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while retrieving the van.";
                cp.content = null;
                return BadRequest(cp);
            }
        }
        // GET: /5
        [HttpGet("{id}")]
        public async Task<ActionResult<Van>> GetVan(int id)
        {
            try
            {
                var Van = await _db.Vans
                    .FirstOrDefaultAsync(s => s.vanId == id);
                if (Van == null)
                {
                    cp.errorMessage = "Van not found.";
                    cp.status = false;
                    cp.message = "No van exists with the provided ID.";
                    cp.content = null;
                    return NotFound(cp);
                }
                cp.errorMessage = null;
                cp.status = true;
                cp.message = "Van retrieved successfully!";
                cp.content = Van;
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while retrieving the van.";
                cp.content = null;
                return BadRequest(cp);
            }
        }
        // PUT: /5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVan(int id, Van van)
        {
            if (id != van.vanId)
            {
                cp.errorMessage = "Invalid van ID.";
                cp.status = false;
                cp.message = "Van ID mismatch.";
                cp.content = null;
                return BadRequest(cp);
            }
            var existingVan = await _db.Vans.FirstOrDefaultAsync(s => s.vanId == id);
            if (existingVan == null)
            {
                cp.errorMessage = "Van not found.";
                cp.status = false;
                cp.message = "No van exists with the provided ID.";
                cp.content = null;
                return NotFound(cp);
            }
            try
            {
                await _db.SaveChangesAsync();
                cp.errorMessage = null;
                cp.status = true;
                cp.message = "Van updated successfully!";
                cp.content = null;
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while updating the van.";
                cp.content = null;
                return BadRequest(cp);
            }
        }
        // POST: 
        [HttpPost]
        public async Task<ActionResult<Van>> PostVan(Van van)
        {
            try
            {
                _db.Vans.Add(van);
                await _db.SaveChangesAsync();

                cp.errorMessage = null;
                cp.status = true;
                cp.message = "Van created successfully!";
                cp.content = van;
                return CreatedAtAction(nameof(GetVan), new { id = van.vanId }, cp);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "Failed to create van.";
                cp.content = null;
                return BadRequest(cp);
            }
        }
        // DELETE: /5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVan(int id)
        {
            try
            {
                var van = await _db.Vans.FindAsync(id);

                if (van == null)
                {
                    cp.errorMessage = "Van not found";
                    cp.status = false;
                    cp.message = "No van exists with the provided ID.";
                    cp.content = null;
                    return NotFound(cp);
                }
                _db.Vans.Remove(van);
                await _db.SaveChangesAsync();
                cp.errorMessage = null;
                cp.status = true;
                cp.message = "Van deleted successfully!";
                cp.content = van;
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while deleting the van.";
                cp.content = null;
                return BadRequest(cp);
            }
        }
        private bool VanExists(int id)
        {
            return _db.Vans.Any(e => e.vanId == id);
        }
    }
}

