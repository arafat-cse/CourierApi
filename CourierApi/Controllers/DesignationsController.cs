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
        //CommanResponse
        private readonly CommanResponse cp = new CommanResponse();
        // GET: api/Designations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Designation>>> GetDesignations()
        {
            //return await _db.Designations.ToListAsync();
            try
            {
                var designations = await _db.Designations.ToListAsync();
                if (designations == null || !designations.Any())
                {
                    cp.errorMessage = null;
                    cp.status = true;
                    cp.message = "No Designation found.";
                    cp.content = null;
                    return Ok(cp);
                }
                cp.errorMessage = null;
                cp.status = true;
                cp.message = "Designation retrieved successfully!";
                cp.content = designations;

                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while retrieving the Designation.";
                cp.content = null;

                return BadRequest(cp);
            }
        }
        // GET: api/Designations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Designation>> GetDesignation(int id)
        {
            try
            {
                // Find the company by ID
                var designation = await _db.Designations.FindAsync(id);

                if (designation == null)
                {
                    cp.errorMessage = "Designation not found";
                    cp.status = false;
                    cp.message = "No Designation exists with the provided ID.";
                    cp.content = null;
                    return NotFound(cp);
                }
                cp.errorMessage = null;
                cp.status = true;
                cp.message = "Designation retrieved successfully!";
                cp.content = designation;
                return Ok(cp);
            }
            catch (Exception ex)
            {
                // Handle exceptions
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while retrieving the Designation.";
                cp.content = null;
                return BadRequest(cp);
            }
        }
        // PUT: api/Designations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDesignation(int id, Designation designation)
        {
            if (id != designation.designationId)
            {
                cp.errorMessage = "Badrequer ID mismatch";
                cp.status = false;
                cp.message = "Designation not found";
                cp.content = null;
                return BadRequest(cp);

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
                    return NotFound("Designation not found");
                }
                else
                {
                    throw;
                }
            }
            return Ok(new { Message = "Designation updated successfully", designationId = id });
        }

        // POST: api/Designations
        [HttpPost]
        public async Task<ActionResult<Designation>> PostDesignation(Designation designation)
        {
            try
            {
                _db.Designations.Add(designation);
                await _db.SaveChangesAsync();

                cp.errorMessage = null; 
                cp.status = true; 
                cp.message = "Company created successfully!";
                cp.content = designation;
                return CreatedAtAction("GetDesignation", new { id = designation.designationId }, designation);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "Failed to create company.";
                cp.content = null;
                return BadRequest(cp);
            }
        }
        // DELETE: api/Designations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDesignation(int id)
        {
          try
            {
                // Find the company by ID
                var designation = await _db.Designations.FindAsync(id);

                if (designation == null)
                {              
                    cp.errorMessage = "designation not found";
                    cp.status = false;
                    cp.message = "No designation exists with the provided ID.";
                    cp.content = null;
                    return NotFound(cp);
                }
                _db.Designations.Remove(designation);
                await _db.SaveChangesAsync();
                cp.errorMessage = null;
                cp.status = true;
                cp.message = "designation deleted successfully!";
                cp.content = designation;
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while deleting the designation.";
                cp.content = null;
                return BadRequest(cp);
            }
        }

        private bool DesignationExists(int id)
        {
            return _db.Designations.Any(e => e.designationId == id);
        }
    }
}
