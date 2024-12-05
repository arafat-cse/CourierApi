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
            //return await _db.Staffs.ToListAsync();
            
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
                cp.content = staff.ToJson();

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

        // GET: api/Staffs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Staff>> GetStaff(int id)
        {
            var staff = await _db.Staffs.FindAsync(id);

            if (staff == null)
            {
                return NotFound();
            }

            return staff;

        }

        // PUT: api/Staffs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStaff(int id, Staff staff)
        {
            if (id != staff.staffId)
            {
                return BadRequest();
            }

            _db.Entry(staff).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(id))
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
            var staff = await _db.Staffs.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }

            _db.Staffs.Remove(staff);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool StaffExists(int id)
        {
            return _db.Staffs.Any(e => e.staffId == id);
        }
    }
}
