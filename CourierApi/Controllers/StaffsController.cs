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
    public class StaffsController : ControllerBase
    {
        private readonly CourierDbContext _db;

        public StaffsController(CourierDbContext context)
        {
            _db = context;
        }
        //CommanResponse
        private readonly CommanResponse cp = new CommanResponse();
        // GET: 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staff>>> GetStaffs()
        {
            try
            {
                var staff = await _db.Staffs.Include(d => d.Designation).ToListAsync();
                if (staff == null || !staff.Any())
                {
                    cp.errorMessage = "No staff found.";
                    cp.status = false; 
                    cp.message = "No staff data available.";
                    cp.content = null;
                    return Ok(cp);
                }
                cp.errorMessage = null;
                cp.status = true;
                cp.message = "Staff retrieved successfully!";
                cp.content = staff;

                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while retrieving the staff.";
                cp.content = null;

                return BadRequest(cp);
            }
        }
        //GET
        [HttpGet("{id}")]
        public async Task<ActionResult<Staff>> GetStaff(int id)
        {
            try
            {
                var staff = await _db.Staffs
                    .Include(s => s.Designation)
                    .FirstOrDefaultAsync(s => s.staffId == id);

                if (staff == null)
                {
                    cp.errorMessage = "Staff not found.";
                    cp.status = false;
                    cp.message = "No staff exists with the provided ID.";
                    cp.content = null;
                    return NotFound(cp);
                }

                // Populate response for a successful retrieval
                cp.errorMessage = null;
                cp.status = true;
                cp.message = "Staff retrieved successfully!";
                cp.content = staff;
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while retrieving the staff.";
                cp.content = null;
                return BadRequest(cp);
            }
        }
        //PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStaff(int id, Staff staff)
        {
            if (id != staff.staffId)
            {
                cp.errorMessage = "Invalid staff ID.";
                cp.status = false;
                cp.message = "Staff ID mismatch.";
                cp.content = null;
                return BadRequest(cp);
            }
            var existingStaff = await _db.Staffs.Include(s => s.Designation).FirstOrDefaultAsync(s => s.staffId == id);
            if (existingStaff == null)
            {
                cp.errorMessage = "Staff not found.";
                cp.status = false;
                cp.message = "No staff exists with the provided ID.";
                cp.content = null;
                return NotFound(cp);
            }

            existingStaff.staffName = staff.staffName;
            existingStaff.email = staff.email;
            existingStaff.updateBy = staff.updateBy;
            existingStaff.updateDate = DateTime.UtcNow;
            existingStaff.IsActive = staff.IsActive;
            existingStaff.designationId = staff.designationId;

            try
            {
                await _db.SaveChangesAsync();
                cp.errorMessage = null;
                cp.status = true;
                cp.message = "Staff updated successfully!";
                cp.content = null; // Optional
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while updating the staff.";
                cp.content = null;
                return BadRequest(cp);
            }
        }
        //POST
        [HttpPost]
        public async Task<ActionResult<Staff>> PostStaff(Staff staff)
        {
            try
            {
                // Validate designation ID
                var designationExists = await _db.Designations.AnyAsync(d => d.designationId == staff.designationId);
                if (!designationExists)
                {
                    cp.errorMessage = "Invalid Designation ID.";
                    cp.status = false;
                    cp.message = "The provided Designation does not exist.";
                    cp.content = null;
                    return BadRequest(cp);
                }

                // Add new staff
                _db.Staffs.Add(staff);
                await _db.SaveChangesAsync();

                cp.errorMessage = null;
                cp.status = true;
                cp.message = "New Staff created successfully!";
                cp.content = staff;

                return CreatedAtAction(nameof(GetStaff), new { id = staff.staffId }, cp);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "Failed to create a new Staff.";
                cp.content = null;
                return BadRequest(cp);
            }
        }
        //DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            var staff = await _db.Staffs.FindAsync(id);
            if (staff == null)
            {
                cp.errorMessage = "Staff not found.";
                cp.status = false;
                cp.message = "No staff exists with the provided ID.";
                cp.content = null;
                return NotFound(cp);
            }

            _db.Staffs.Remove(staff);
            await _db.SaveChangesAsync();

            cp.errorMessage = null;
            cp.status = true;
            cp.message = "Staff deleted successfully!";
            cp.content = null;

            return Ok(cp);
        }

    }
}
