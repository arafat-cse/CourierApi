using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourierApi.Data;
using CourierApi.Models;
using NuGet.Protocol;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CourierApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly CourierDbContext _db;

        public StaffsController(CourierDbContext db)
        {
            _db = db;
        }
        //CommanResponse
        CommanResponse cp = new CommanResponse();

        // GET: All Staffs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staff>>> GetStaffs()
        {
            try
            {

                var staff = await _db.Staffs.ToListAsync();
                if (staff == null || !staff.Any())
                {
                    cp.errorMessage = null;
                    cp.status = true;
                    cp.message = "No staff found.";
                    cp.content = null;
                    return Ok(cp);
                }

                // Populate response for a successful find
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
                cp.message = "An error occurred while any retrieving the staff.";
                cp.content = null;

                return BadRequest(cp);
            }
        }

        // 2. GET a staff by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Staff>> GetStaff(int id)
        {
            try
            {
                // Find the staff by ID
                var staff = await _db.Staffs.FindAsync(id);

                if (staff == null)
                {
                    cp.errorMessage = "staff not found";
                    cp.status = false;
                    cp.message = "No staff exists with the provided ID.";
                    cp.content = null;
                    return NotFound(cp);
                }

                // Populate response for a successful retrieval
                cp.errorMessage = null;
                cp.status = true;
                cp.message = "staff retrieved successfully!";
                cp.content = staff;
                return Ok(cp);
            }
            catch (Exception ex)
            {
                // Handle exceptions
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while retrieving any staff.";
                cp.content = null;
                return BadRequest(cp);
            }

        }
        // 3. POST a New Staff
        [HttpPost]
        public async Task<ActionResult<Staff>> PostStaff(Staff staff)
        {
            try
            {
                _db.Staffs.Add(staff);
                await _db.SaveChangesAsync();

                cp.errorMessage = null; // No error since the operation is successful
                cp.status = true; // Success status
                cp.message = "New Staff Created successfully!";
                cp.content = staff;

                // Returning the common response with CreatedAtAction
                return CreatedAtAction(nameof(GetStaff), new { id = staff.staffId }, cp);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "Failed to Create a New Staff.";
                cp.content = null;
                return BadRequest(cp);
            }

        }
        // 4. PUT Update a Staff

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStaff(int id, Staff staff)
        {
            if (id != staff.staffId)
            {
                cp.errorMessage = "Badrequer ID mismatch";
                cp.status = false;
                cp.message = "staff not found";
                cp.content = null;
                return BadRequest(cp);

            }
            _db.Entry(staff).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_db.Staffs.Any(c => c.staffId == id))
                {
                    return NotFound("Staff not found");
                }
                else
                {
                    throw;
                }
            }


            return Ok(new { Message = "New Staff updated successfully", staffId = id });

            return NoContent();
        }

        // POST: api/Staffs
        [HttpPost]
        public async Task<ActionResult<Staff>> PostStaff(Staff staff)
        {
            _db.Staffs.Add(staff);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetStaff", new { id = staff.staffId }, staff);

        }

        // DELETE: api/Staffs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            CommanResponse de = new CommanResponse();

            try
            {
                // Find the staff by ID
                var staff = await _db.Staffs.FindAsync(id);

                if (staff == null)
                {
                    // Staff is not found response
                    de.errorMessage = "Staff is not found";
                    de.status = false;
                    de.message = "No Staff exists with the provided ID.";
                    de.content = null;
                    return NotFound(de);
                }

                // Remove the Staff and save changes
                _db.Staffs.Remove(staff);
                await _db.SaveChangesAsync();

                // Populate success response
                de.errorMessage = null;
                de.status = true;
                de.message = "Satff deleted successfully!";
                de.content = staff;
                return Ok(de);
            }
            catch (Exception ex)
            {
                de.errorMessage = ex.Message;
                de.status = false;
                de.message = "An error occurred while deleting the company.";
                de.content = null;
                return BadRequest(de);
            }
        }
    }
}
