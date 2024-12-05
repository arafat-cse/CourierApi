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
    public class BranchStaffsController : ControllerBase
    {
        private readonly CourierDbContext _db;

        public BranchStaffsController(CourierDbContext db)
        {
            _db = db;
        }
        //CommanResponse
        CommanResponse cp = new CommanResponse();
        // GET: api/BranchStaffs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BranchStaff>>> GetBranchesStaffs()
        {
            try
            {

                var branchStaffs = await _db.BranchesStaffs.ToListAsync();
                if (branchStaffs == null || !branchStaffs.Any())
                {
                    cp.errorMessage = null;
                    cp.status = true;
                    cp.message = "No BranchStaff found.";
                    cp.content = null;
                    return Ok(cp);
                }

                // Populate response for a successful find
                cp.errorMessage = null;
                cp.status = true;
                cp.message = "BranchStaff retrieved successfully!";
                cp.content = branchStaffs;

                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while retrieving the BranchStaff.";
                cp.content = null;

                return BadRequest(cp);
            }
        }
            // GET: api/BranchStaffs/5
            [HttpGet("{id}")]
        public async Task<ActionResult<BranchStaff>> GetBranchStaff(int id)
        {
          
            try
            {
                // Find the company by ID
                var branchStaff = await _db.BranchesStaffs.FindAsync(id);

                if (branchStaff == null)
                {
                    cp.errorMessage = "BranchesStaff not found";
                    cp.status = false;
                    cp.message = "No BranchesStaff exists with the provided ID.";
                    cp.content = null;
                    return NotFound(cp);
                }

                // Populate response for a successful retrieval
                cp.errorMessage = null;
                cp.status = true;
                cp.message = "BranchesStaff retrieved successfully!";
                cp.content = branchStaff;
                return Ok(cp);
            }
            catch (Exception ex)
            {
                // Handle exceptions
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while retrieving the BranchesStaff.";
                cp.content = null;
                return BadRequest(cp);
            }
        }
        // PUT: api/BranchStaffs/5 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBranchStaff(int id, BranchStaff branchStaff)
        {
            if (id != branchStaff.branchStaffId)
            {
                return BadRequest();
            }

            _db.Entry(branchStaff).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchStaffExists(id))
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

        // POST: api/BranchStaffs      
        [HttpPost]
        public async Task<ActionResult<BranchStaff>> PostBranchStaff(BranchStaff branchStaff)
        {
            _db.BranchesStaffs.Add(branchStaff);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetBranchStaff", new { id = branchStaff.branchStaffId }, branchStaff);
        }

        // DELETE: api/BranchStaffs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranchStaff(int id)
        {
            var branchStaff = await _db.BranchesStaffs.FindAsync(id);
            if (branchStaff == null)
            {
                return NotFound();
            }

            _db.BranchesStaffs.Remove(branchStaff);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool BranchStaffExists(int id)
        {
            return _db.BranchesStaffs.Any(e => e.branchStaffId == id);
        }
    }
}
